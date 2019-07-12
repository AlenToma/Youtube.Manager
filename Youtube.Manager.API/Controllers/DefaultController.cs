using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using Youtube.Manager.API.Models;
using Youtube.Manager.Core;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using System.Linq;
using Youtube.Manager.API.Controllers.BaseController;

namespace Youtube.Manager.API.Controllers
{
    public class DefaultController : AdminAPController
    {
        // we may need to add some changes here later as if now, the Google provider
        // take care of the login
        [AllowAnonymous]
        public System.Web.Mvc.ActionResult Google(object error = null)
        {
            //var test = Request.IsAuthenticated;

            return new System.Web.Mvc.EmptyResult();
        }

        /// <summary>
        ///     Get the user or create new if not exist
        /// </summary>
        /// <param name="email"></param>
        /// <param name="imageUrl"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public User LogIn(string email, string imageUrl, string password)
        {
            var regexp = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" +
                                   @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!regexp.IsMatch(email))
                throw new Exception("Error: Email is not Valid");

            using (var db = new DbRepository())
            {
                var pss = new DataCipher().Encrypt(password);
                //var pss = Actions.Encrypt(password);
                var user = db.Get<User>().Where(x => x.Email.Contains(email) && pss == x.Password).LoadChildren()
                    .IgnoreChildren(x => x.VideoCategories.Select(a => a.Videos)).ExecuteFirstOrDefault();
                if (user == null)
                {
                    user = new User
                    {
                        Email = email,
                        Password = pss,
                        Picture = imageUrl,
                        VideoCategories = new List<VideoCategory>()
                    };

                    db.Save(user).SaveChanges();
                    new DirectoryManager(Actions.ImageRootPath, Actions.GenerateUserFolderName(user.Email)).Create();
                }
                else
                {
                    user.VideoCategories.ForEach(x =>
                    {
                        x.TotalVideos = db.Get<VideoData>().Where(a => a.Category_Id == x.EntityId).ExecuteCount();
                    });
                }

                return user;
            }
        }

        /// <summary>
        ///     Vote a video or a category
        /// </summary>
        /// <param name="type"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <param name="id"></param>
        [HttpPost]
        public void Vote(VideoSearchType type, bool up, bool down, long id)
        {
            using (var db = new DbRepository())
            {
                var v = type == VideoSearchType.Videos
                    ? db.Get<VideoData>().Where(x => x.EntityId == id).ExecuteFirstOrDefault() as dynamic
                    : db.Get<VideoCategory>().Where(x => x.EntityId == id).ExecuteFirstOrDefault() as dynamic;
                v.Up_Vote += up ? 1 : 0;
                v.Down_Vote += down ? 1 : 0;
                db.Save(v);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///     GetVideoCategory ByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<VideoCategory>> GetVideoCategory(long? userId = null, long? categoryId = null)
        {
            using (var db = new DbRepository())
            {
                return await db.Get<VideoCategory>().LoadChildren().IgnoreChildren(x => x.Videos).Where(x =>
                        (!userId.HasValue || x.User_Id == userId) && (!categoryId.HasValue || categoryId == x.EntityId))
                    .ExecuteAsync();
            }
        }

        /// <summary>
        ///     Get Video by video_Id or Category
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="categoryId"></param>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<VideoData>> GetVideoData(string videoId, long? categoryId = null, long? userId = null, int page = 1)
        {
            using (var db = new DbRepository())
            {
                return await db.Get<VideoData>()
                    .Where(x => (string.IsNullOrEmpty(videoId) || videoId == x.Video_Id)
                                &&
                                (!categoryId.HasValue || categoryId == x.Category_Id)
                                &&
                                (!userId.HasValue || x.VideoCategory.User_Id == userId))
                    .Skip((page - 1) * 30).Take(30).ExecuteAsync();
            }
        }

        /// <summary>
        ///     SaveVideoData
        /// </summary>
        /// <param name="Id"></param>
        [HttpPost]
        public void SaveVideo(VideoData video)
        {
            using (var db = new DbRepository())
            {
                db.Save(video).SaveChanges();
            }
        }

        /// <summary>
        ///     Save VideoCategory
        /// </summary>
        /// <param name="videoCategories"></param>
        [HttpPost]
        public void SaveCategory(List<VideoCategory> videoCategories)
        {
            using (var db = new DbRepository())
            {
                try
                {
                    videoCategories.ForEach(x =>
                    {
                        var user = db.Get<User>().Where(u => u.EntityId == x.User_Id).ExecuteFirstOrDefault();

                        if (x.State == State.Removed)
                        {
                            db.Get<VideoCategory>().Where(a => a.EntityId == x.EntityId).LoadChildren().Remove();
                            if (!string.IsNullOrEmpty(x.Logo) && System.IO.File.Exists(
                                    Path.Combine(Actions.ImageRootPath, Actions.GenerateUserFolderName(user.Email),
                                        x.Logo)))
                                System.IO.File.Exists(Path.Combine(Actions.ImageRootPath,
                                    Actions.GenerateUserFolderName(user.Email), x.Logo));
                        }
                        else
                        {
                            db.Save(x);


                            if (x.EntityId.HasValue)
                            {
                                var videos = db.Get<VideoData>().Where(a => a.Category_Id == x.EntityId).Skip(0).Take(5)
                                    .Execute();
                                if (videos.Count() >= 1 && videos.Count() <= 4)
                                {
                                    if (!string.IsNullOrEmpty(x.Logo) &&
                                        System.IO.File.Exists(Path.Combine(Actions.ImageRootPath, x.Logo)))
                                        System.IO.File.Exists(Path.Combine(Actions.ImageRootPath, x.Logo));

                                    var fName = $"{Actions.GenerateUserFolderName(user.Email)}\\{Guid.NewGuid()}.png";
                                    var path = Path.Combine(Actions.ImageRootPath, fName);
                                    var file = Helper.CombineImages(videos.Select(a => a.ThumpUrl).ToArray());
                                    System.IO.File.WriteAllBytes(path, file);
                                    x.Logo = fName;
                                }

                                x.Videos.ForEach(v =>
                                {
                                    if (db.Get<VideoData>()
                                        .Where(a => a.Video_Id == v.Video_Id && v.EntityId != a.EntityId).ExecuteAny())
                                        v.State = State.Removed;
                                });
                                db.Save(x);
                            }
                        }
                    });
                }
                catch (Exception e)
                {
                    db.Rollback();
                    throw e;
                }
                finally
                {
                    db.SaveChanges();
                }
            }
        }
    }
}