using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaManager.Forms;
using Rest.API.Translator;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.Attributes;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using Realm.Of.Y.Manager.Models.Container.Interface;
using Realm.Of.Y.Manager.Views;

namespace Realm.Of.Y.Manager.Helper
{
    public static class Methods
    {

        public static IMainActivity AppSettings { get; set; }

        /// <summary>
        ///     this is a global notificate <videoId, progress />
        /// </summary>
        public static Action<string, int> OnFileProgress;


        public static Action<VideoView> OnVideoView;

        /// <summary>
        ///     Get the top View
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private static View funcChild;

        /// <summary>
        ///     Load all Converter dynamicly.
        ///     You only need to create IValueConverter and add [ClassKey] and this will add it automatically
        /// </summary>
        /// <param name="page"></param>
        private static List<TypeInfo> _chachedTypes = new List<TypeInfo>();

        /// <summary>
        ///     Start loader popup
        /// </summary>
        /// <param name="page"></param>
        private static readonly List<LoadingPopupPage> _cachedLoader = new List<LoadingPopupPage>();

        public static MixVideoPlayer ApplicationPlayer;
        /// <summary>
        ///     DataBinder Will be triggered when an operation to the api has been called
        /// </summary>
        /// <param name="moduleTrigger"></param>
        /// <param name="apiControllerMapping"></param>
        /// <returns></returns>
        public static ModuleTrigger AddTrigger(this ModuleTrigger moduleTrigger, params MethodInformation[] apiControllerMapping)
        {
            apiControllerMapping.ToList().ForEach(a =>
            {
                if (!ObjectCacher.ModuleTriggerCacher.ContainsKey(moduleTrigger))
                    ObjectCacher.ModuleTriggerCacher.Add(moduleTrigger, new List<string> { a.ToString() });
                else if (!ObjectCacher.ModuleTriggerCacher[moduleTrigger].Any(x => x == a.ToString()))
                    ObjectCacher.ModuleTriggerCacher[moduleTrigger].Add(a.ToString());
            });
            return moduleTrigger;
        }

        public static void RemoveTrigger(this ModuleTrigger moduleTrigger)
        {
            if (ObjectCacher.ModuleTriggerCacher.ContainsKey(moduleTrigger))
                ObjectCacher.ModuleTriggerCacher.Remove(moduleTrigger);
        }

        public static async Task ExecuteTrigger(this MethodInformation methodInformation)
        {
            var triggers = ObjectCacher.ModuleTriggerCacher.Where(x => x.Value.Any(a => a == methodInformation.ToString())).ToList();
            foreach (var x in triggers) await x.Key.DataBinder(methodInformation);
        }

        public static int GetRandomId()
        {
            var id = ObjectCacher.RandomIdCacher.Keys.Any() ? ObjectCacher.RandomIdCacher.Keys.Max() + 1 : 100;
            if (id <= 0)
                return GetRandomId();
            ObjectCacher.RandomIdCacher.Add(id, id);
            return id;
        }

        public static T GetAbsoluteParent<T>(this View view) where T : View
        {
            return (T)view.GetAbsoluteParent(v => v is T);
        }

        public static View GetAbsoluteParent(this View view, Func<View, bool> func = null)
        {
            if (func != null && func(view))
                return view;
            funcChild = view;
            if (view.Parent is View) return GetAbsoluteParent(view.Parent as View, func);

            if (func == null || func(view.Parent as View))
            {
                funcChild = null;
                return view;
            }

            var v = funcChild;
            funcChild = null;
            return v;
        }


        /// <summary>
        ///     Return embedded file from IProduct.Modules.SQL
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetResources(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Youtube.Manager.Resources.{fileName}";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                return result;
            }
        }

        public static List<VideoWrapper> ToItemList(this YoutubeVideoCollection items)
        {
            var result = new List<VideoWrapper>();
            result.AddRange(items.Videos);
            result.AddRange(items.Playlists);
            result.AddRange(items.Albums);
            return result;
        }

        public static void LoadValueConverters(this ILayout page)
        {
            var resourceDictionary = (page as ContentPage)?.Resources;
            if (resourceDictionary == null)
                resourceDictionary = (page as ContentView)?.Resources;

            if (resourceDictionary == null)
                return;
            if (!_chachedTypes.Any())
                _chachedTypes = typeof(Methods).Assembly.DefinedTypes.Where(x =>
                    x.GetCustomAttribute<ClassKey>() != null && typeof(IValueConverter).IsAssignableFrom(x)).ToList();

            _chachedTypes.ForEach(x =>
            {
                var key = x.GetCustomAttribute<ClassKey>();
                if (!resourceDictionary.ContainsKey(key.Name))
                    resourceDictionary.Add(key.Name, Activator.CreateInstance(x));
            });
        }

        /// <summary>
        ///     Clear all Open loaders
        /// </summary>
        public static void ClearLoader()
        {
            while (_cachedLoader.Any())
                EndLoading(_cachedLoader.First());
        }

        /// <summary>
        ///     Close popup
        /// </summary>
        /// <param name="page"></param>
        public static async void EndLoading(this LoadingPopupPage page)
        {
            if (_cachedLoader.IndexOf(page) < 0)
                return;
            _cachedLoader.Remove(page);
            await PopupNavigation.Instance.RemovePageAsync(page, true);
        }

        public static async Task<LoadingPopupPage> StartLoading(this ILayout page)
        {
            var loader = new LoadingPopupPage();
            _cachedLoader.Add(loader);
            await PopupNavigation.Instance.PushAsync(loader, true);
            return loader;

        }

        /// <summary>
        ///     End loader popup
        /// </summary>
        /// <param name="page"></param>
        public static async Task Close(this PopupPage page)
        {
            await PopupNavigation.Instance.RemovePageAsync(page, true);
        }

        /// <summary>
        ///     Close popup
        /// </summary>
        /// <param name="page"></param>
        public static async void Open(this PopupPage page, bool animate = true)
        {
            if (animate)
                page.Animation = new ScaleAnimation
                {
                    PositionIn = MoveAnimationOptions.Bottom,
                    PositionOut = MoveAnimationOptions.Center,
                    ScaleIn = 1,
                    ScaleOut = 0.7,
                    DurationIn = 700,
                    EasingIn = Easing.BounceOut
                };

            await PopupNavigation.Instance.PushAsync(page, animate);
            //return page;
        }


        public static async Task DownloadCompleted(string fullPath)
        {
            var data = ParseLocalVideoPath(fullPath);
            if (data != null)
            {
                data = ObjectCacher.DownloadingFiles.ContainsKey(data.VideoId)
                    ? ObjectCacher.DownloadingFiles[data.VideoId]
                    : data;
                if (ObjectCacher.DownloadingFiles.ContainsKey(data.VideoId))
                    ObjectCacher.DownloadingFiles.Remove(data.VideoId);
                var category = ControllerRepository.Db(x => x.GetVideoCategory(UserData.CurrentUser.EntityId.Value, data.category_Id)).FirstOrDefault();
                if (category != null)
                {
                    category.Videos = new List<VideoData>();
                    category.Videos.Add(new VideoData
                    {
                        Video_Id = data.VideoId,
                        Title = data.Title,
                        ThumpUrl = data.ThumpUrl,
                        Quality = data.Quality,
                        Duration = data.Duration,
                        Description = data.Description,
                        Auther = data.Auther,
                        Resolution = data.Resolution

                    });
                    /// Save the changes to DownloadCoins, that has been made in FileService
                    await UserData.SaveUserChanges();
                    await UserData.SaveCategory(category);
                }
            }
        }

        /// <summary>
        ///     Parse local path
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static YoutubeFileDownloadItem ParseLocalVideoPath(string url)
        {
            YoutubeFileDownloadItem res = null;
            var reg = new Regex(@"\[(.*?)\]");
            var matches = reg.Matches(url);
            if (matches.Count >= 3) // [I=][F=18][P=]
            {
                res = new YoutubeFileDownloadItem
                {
                    VideoId = matches.Cast<Match>().FirstOrDefault()?.Value?.Replace("I=", "").Replace("[", "")
                        .Replace("]", ""),
                    category_Id = Convert.ToInt64(matches.Cast<Match>().LastOrDefault().Value?.Replace("P=", "")
                        .Replace("[", "").Replace("]", ""))
                };

                res.Title = url.Substring(url.LastIndexOf('/') + 1);
            }

            return res;
        }

        /// <summary>
        ///     Prepare a downloadable object
        /// </summary>
        /// <param name="youtubeItem"></param>
        /// <param name="videoCategory"></param>
        /// <returns></returns>
        public static YoutubeFileDownloadItem GetDownloadableItem(this VideoData youtubeItem)
        {
            var videoCategory = ControllerRepository.Db(x => x.GetVideoCategory(UserData.CurrentUser.EntityId, youtubeItem.Category_Id)).FirstOrDefault();

            if (youtubeItem.Video_Id.ToLower().Contains("youtube"))
                youtubeItem.Video_Id = youtubeItem.Video_Id.Split('=').Last();
            var v = new YoutubeFileDownloadItem();
            v.Playlist = videoCategory?.Name;
            v.VideoId = youtubeItem.Video_Id;
            v.Title = youtubeItem.Title;
            v.category_Id = videoCategory?.EntityId;
            v.ThumpUrl = youtubeItem.ThumpUrl;
            return v;
        }

        /// <summary>
        ///     Generate localPath from YoutubeFileDownloadItem
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public static string GenerateLocalPath(this YoutubeFileDownloadItem video, YoutubeVideoInfo youtubeVideoInfo)
        {
            return
                $"{video.Title.Replace("[", "{").Replace("]", "}")}[I={video.VideoId}][F={youtubeVideoInfo?.FormatCode ?? 18}][P={video.category_Id}].mp4";
        }


        /// <summary>
        ///     Prepare a downloadable object
        /// </summary>
        /// <param name="youtubeItem"></param>
        /// <param name="videoCategory"></param>
        /// <returns></returns>
        public static YoutubeFileDownloadItem GetDownloadableItem(this VideoWrapper youtubeItem, VideoCategory videoCategory)
        {
            var v = new YoutubeFileDownloadItem();
            v.Playlist = videoCategory.Name;
            v.VideoId = youtubeItem.Id;
            v.Title = youtubeItem.Title;
            v.category_Id = videoCategory.EntityId;
            v.ThumpUrl = youtubeItem.DefaultThumbnailUrl;
            return v;
        }


        public static bool UserIsLogedIn(this ILayout page, bool showMessage = false)
        {
            if (UserData.CurrentUser == null)
                if (showMessage)
                    Application.Current.MainPage.DisplayAlert("LoginError".GetString(), "", "Ok".GetString());

            return UserData.CurrentUser != null;
        }




        /// <summary>
        ///     Download the video, also checks for an existing user and playlist
        /// </summary>
        /// <param name="page"></param>
        /// <param name="video"></param>
        public static async void Download(this ILayout page, VideoWrapper video, PageType pageType = PageType.Popup)
        {



            if (UserData.CurrentUser == null)
            {
                await Application.Current.MainPage.DisplayAlert(null, "NotLoggedIn".GetString(), "Ok".GetString());
                return; // break
            }


            if (!UserData.VideoCategoryViews.Any())
            {
                var action = await Application.Current.MainPage.DisplayActionSheet("PCreate".GetString(), null, "Create".GetString());
                if (action == "Create".GetString())
                {
                    var catId = await UserData.CreateEditPlayList();
                    if (catId > 0)
                    {
                        if (!video.IsVideo)
                        {
                            var vView = await new VideosProperties(video, catId).DataBind();
                            vView.Open();
                        }
                        else
                        {
                            if (UserData.CanDownload(true))
                                Methods.AppSettings.DownloadVideo(video.GetDownloadableItem(UserData.VideoCategoryViews.Find(x => x.EntityId == catId)));
                        }
                    }
                }
            }
            else
            {
                if (pageType == PageType.Popup || !video.IsVideo)
                {
                    var temp = new DataTemplate(() =>
                    {
                        var cellView = new ViewCell();
                        var text = new Label
                        {
                            Style = (Style)Application.Current.Resources["Header"]
                        };
                        text.SetBinding(Label.TextProperty, "Name");

                        cellView.View = new StackLayout
                        {
                            Children = { text },
                            Style = (Style)Application.Current.Resources["FormFloatLeft"]
                        };
                        return cellView;
                    });
                    var lst = new YListView
                    {
                        ItemsSource = UserData.VideoCategoryViews,
                        ItemTemplate = temp
                    };

                    var btnClose = new CustomButton
                    {
                        Text = "Close".GetString(),
                        Style = (Style)Application.Current.Resources["ButtonContainer"]
                    };

                    var btnCreate = new CustomButton
                    {
                        Text = "Create".GetString(),
                        Style = (Style)Application.Current.Resources["ButtonContainer"]
                    };

                    var stk = new StackLayout
                    {
                        Style = (Style)Application.Current.Resources["PopUpCenter"],
                        Children =
                        {
                            new StackLayout
                            {
                                Style = (Style) Application.Current.Resources["FormFloatLeft"],
                                BackgroundColor = (Color) Application.Current.Resources["barBackgroundColor"],
                                Children =
                                {
                                    new Label
                                    {
                                        Text = "PChoose".GetString(),
                                        Style = (Style) Application.Current.Resources["HeaderContainer"]
                                    }
                                }
                            },

                            new StackLayout
                            {
                                Children = {lst},
                                BackgroundColor = (Color) Application.Current.Resources["applicationColor"]
                            },
                            new StackLayout
                            {
                                Style = (Style) Application.Current.Resources["FormFloatLeft"],
                                BackgroundColor = (Color) Application.Current.Resources["barBackgroundColor"],
                                Children =
                                {
                                    btnClose, // Cancel
                                    btnCreate // Create new playlist
                                }
                            }
                        }
                    };

                    var view = new PopupPage
                    {
                        Content = stk
                    };
                    btnClose.Clicked += async (o, e) => await view.Close();
                    lst.OnSelected += async (o, l) =>
                    {
                        var loader = await page.StartLoading();
                        var category = o as VideoCategory;
                        // Download the video
                        if (!video.IsVideo)
                        {
                            var vView = await new VideosProperties(video, category.EntityId.Value).DataBind();
                            vView.Open();
                            await view.Close();
                        }
                        else
                        {
                            if (UserData.CanDownload(true))
                                Methods.AppSettings.DownloadVideo(video.GetDownloadableItem(category));
                            await view.Close();
                        }

                        loader.EndLoading();
                    };

                    btnCreate.Clicked += async (o, e) =>
                    {
                        var catId = await UserData.CreateEditPlayList();
                        if (catId > 0)
                        {
                            if (!video.IsVideo)
                            {
                                var vView = await new VideosProperties(video, catId).DataBind();
                                vView.Open();
                                await view.Close();
                            }
                            else
                            {
                                if (UserData.CanDownload(true))
                                    Methods.AppSettings.DownloadVideo(video.GetDownloadableItem(UserData.VideoCategoryViews.Find(x => x.EntityId == catId)));
                            }
                        }
                    };
                    view.Open();
                }
                else
                {
                    var buttons = UserData.VideoCategoryViews.Select(x => x.Name).ToList();
                    buttons.Insert(0, "Create".GetString());
                    var action = await Application.Current.MainPage.DisplayActionSheet("PChoose".GetString(), "Close".GetString(), null, buttons.ToArray());

                    if (action == "Create".GetString())
                    {
                        var catId = await UserData.CreateEditPlayList();
                        if (catId > 0)
                        {
                            if (UserData.CanDownload(true))
                                Methods.AppSettings.DownloadVideo(video.GetDownloadableItem(UserData.VideoCategoryViews.Find(x => x.EntityId == catId)));
                        }
                    }
                    else
                    {
                        var cat = UserData.VideoCategoryViews.FirstOrDefault(x => x.Name == action);
                        if (cat != null && UserData.CanDownload(true))
                            Methods.AppSettings.DownloadVideo(video.GetDownloadableItem(cat));
                    }
                }
            }
        }
    }
}