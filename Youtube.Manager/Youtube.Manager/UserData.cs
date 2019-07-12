using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Youtube.Manager.Controls;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;
using Youtube.Manager.Models.Container.Interface.API;
using Youtube.Manager.Models.views;

namespace Youtube.Manager
{
    public static class UserData
    {
        public static bool Notified { get; set; }

        public static User CurrentUser { get; private set; }

        public static List<VideoCategoryView> VideoCategoryViews { get; private set; }

        public static DirectoryManager DirectoryManager { get; set; }

        public static async Task LogIn(string email, string password = "google.com", string imageUrl = "")
        {
            DirectoryManager = null;
            MethodInformation info = null;
            ControllerRepository.Db(x => x.LogIn(email, imageUrl, password), (x, data) => { CurrentUser = data; info = x; });
            if (CurrentUser != null)
            {
                await Methods.AppSettings.ValidateStoragePermission();
                Methods.AppSettings.UserLocalSettings.UserName = CurrentUser.Email;
                Methods.AppSettings.UserLocalSettings.Password = CurrentUser.Password;
                Methods.AppSettings.UserLocalSettings.Image = CurrentUser.Picture;
            }

            await LoadUserData(true);
            await info.ExecuteTrigger();
        }


        public static bool CanDownload(bool showAlternative = false)
        {
            if (CurrentUser.DownloadCoins > 0 || CurrentUser.UserType == UserType.Primary)
                return true;

            if (showAlternative)
            {

                new BuyCoins();

            }
            return false;
        }


        public static async Task SaveUserChanges()
        {
            MethodInformation info = null;
            CurrentUser = ControllerRepository.Db(x => x.SaveUser(CurrentUser), (u, i) => { info = u; });
            await info?.ExecuteTrigger();
        }

        public static async void SaveVideo(params VideoData[] videos)
        {

            foreach (var v in videos)
            {
                ControllerRepository.Db(x => x.SaveVideo(v)).Await();
                if (v.State == State.Removed)
                {
                    var cat = ControllerRepository.Db(x => x.GetVideoCategory(null, v.Category_Id)).FirstOrDefault();
                    var directoryManager = UserData.DirectoryManager.Folder(cat.Name).Create();
                    foreach (var file in directoryManager.GetFiles(System.IO.SearchOption.TopDirectoryOnly, v.Video_Id))
                        file.File.Delete();
                }
            }
            await UserData.LoadUserData(true);
            await ControllerRepository.GetInfo<IDbController, Task>((x) => x.SaveVideo(null)).ExecuteTrigger();
        }


        /// <summary>
        ///     Save VideoCategory
        /// </summary>
        /// <param name="videoCategories"></param>
        public static async Task<long> SaveCategory(VideoCategory videoCategory)
        {
            MethodInformation info = null;
            if (videoCategory.State == State.Removed)
                UserData.DirectoryManager.Folder(videoCategory.Name).Delete();
            var id = ControllerRepository.Db(x => x.SaveCategory(videoCategory), (i, x) => info = i).Await();
            await LoadUserData(true);
            await info.ExecuteTrigger();
            return await Task.FromResult(id);

        }

        // try and create new playlist

        public static async Task<long> CreateEditPlayList(VideoCategory editedVideoCategory = null)
        {
            var editMode = editedVideoCategory?.EntityId.HasValue ?? false;
            var orgName = editedVideoCategory?.Name;
            var id = await CreatePlayList(editedVideoCategory);
            if (id > 0)
            {
                if (editMode)
                {
                    DirectoryManager.Folder(orgName).ReName(VideoCategoryViews.Find(x => x.EntityId == id).Name);
                }
                else DirectoryManager.Folder(VideoCategoryViews.Find(x => x.EntityId == id).Name).Create();
            }

            return id;
        }

        private static async Task<long> CreatePlayList(VideoCategory editedVideoCategory = null, string errorMessage = null)
        {
            editedVideoCategory = editedVideoCategory ?? new VideoCategory() { User_Id = UserData.CurrentUser.EntityId.Value };
            var res = await UserDialogs.Instance.PromptAsync(errorMessage ?? "", "PCreate".GetString(), "Save".GetString(), "Cancel".GetString(), "PPlaceHolderName".GetString(), InputType.Name);
            errorMessage = "";
            if (res.Ok)
            {
                if (string.IsNullOrEmpty(res.Text))
                {
                    return await CreatePlayList(editedVideoCategory, "PPlaceHolderName".GetString() + " cannot be empty");
                }

                if (UserData.VideoCategoryViews.Any(x => editedVideoCategory.EntityId != x.EntityId && string.Equals(x.Name, res.Text, StringComparison.OrdinalIgnoreCase)))
                {
                    return await CreatePlayList(editedVideoCategory, "PPlaceHolderName".GetString() + " already exist");
                }

                editedVideoCategory.Name = res.Text;
                var id = await UserData.SaveCategory(editedVideoCategory);
                return id;
            }
            else return -1;
        }



        /// <summary>
        /// Reload/Load the userData
        /// </summary>
        /// <returns></returns>
        public static async Task LoadUserData(bool forceReload = true)
        {
            if (CurrentUser == null)
                return;
            if (forceReload)
                VideoCategoryViews = CurrentUser?.EntityId.HasValue ?? false ? ControllerRepository.Db(x => x.GetVideoCategory(CurrentUser.EntityId, null)) : new List<VideoCategoryView>();

        }
    }
}