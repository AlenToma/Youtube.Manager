using EntityWorker.Core.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;

namespace Realm.Of.Y.Manager.Core.API.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DbController : ControllerBase
    {
        [HttpGet]
        public List<VideoCategoryView> GetVideoCategory(long? userId = null, long? categoryId = null)
        {
            using (var db = new DbRepository())
            {
                return (List<VideoCategoryView>)db.GetSqlCommand(Actions.GetSQL("VideoCategoryView.sql"))
                    .AddInnerParameter("@UserId", userId)
                    .AddInnerParameter("@CategoryId", categoryId)
                    .DataReaderConverter(typeof(VideoCategoryView));
            }
        }

        [HttpGet]
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
                    .Skip((page - 1) * 1000).Take(1000).ExecuteAsync();
            }
        }

        [HttpPost]
        public User SaveUser(User user)
        {
            using (var db = new DbRepository())
            {
                db.Save(user).SaveChanges();
                return user;
            }
        }


        [HttpGet]
        public User LogIn(string email, string imageUrl, string password)
        {
            var regexp = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" +
                            @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!regexp.IsMatch(email))
                throw new Exception("Error: Email is not Valid");
            var cypher = new DataCipher();
            using (var db = new DbRepository())
            {
                var pss = !cypher.IsEncrypt(password) ? new DataCipher().Encrypt(password) : password;
                var user = db.Get<User>().Where(x => x.Email.Contains(email) && pss == x.Password).ExecuteFirstOrDefault();
                if (user == null && password == "google.com")
                {
                    user = new User
                    {
                        Email = email,
                        Password = cypher.Encrypt(Guid.NewGuid().ToString("N")),
                        Picture = imageUrl
                    };

                    db.Save(user).SaveChanges();
                    new DirectoryManager(Actions.ImageRootPath, Actions.GenerateUserFolderName(user.Email)).Create();
                }
                return user;
            }
        }

        [HttpGet]
        public List<VideoCategoryView> GetUserSuggestion(long userId)
        {
            using (var db = new DbRepository())
                return db.GetUserSuggestion(userId);
        }

        [HttpPost]
        public long SaveCategory(VideoCategory videoCategory)
        {
            using (var db = new DbRepository())
            {
                try
                {
                    var x = videoCategory;
                    if (videoCategory.State == State.Removed)
                    {
                        db.Get<VideoCategory>().Where(a => a.EntityId == x.EntityId).IgnoreChildren(a => a.Videos.Select(v => v.VideoCategory), a => a.Videos.Select(v => v.Rating), a => a.Rating).LoadChildren().Remove();
                    }
                    else
                    {

                        var videos = x.Videos;
                        x.Videos = new List<VideoData>();
                        if (videos?.Any() ?? false)
                        {
                            db.Save(x);
                            foreach (var v in videos)
                            {
                                v.Category_Id = x.EntityId.Value;
                                SaveVideo(v);
                            }
                        }
                        else
                            db.Save(x);
                    }

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

                return videoCategory.EntityId ?? -1;
            }
        }

        [HttpPost]
        public void SaveVideo(VideoData video)
        {
            using (var db = new DbRepository())
            {
                if (!video.EntityId.HasValue && db.Get<VideoData>().Where(x => x.Video_Id == video.Video_Id && x.Category_Id == video.Category_Id).ExecuteAny())
                {
                    return; // dont save, its already exist
                }
                db.Save(video).SaveChanges();
            }
        }

        public List<RatingModelView> GetItemRating(VideoSearchType type, List<long> ids)
        {
            using (var db = new DbRepository())
            {
                var result = new List<RatingModelView>();
                foreach (var id in ids)
                {
                    result.Add(new RatingModelView()
                    {
                        Entity_Id = id,
                        VideoSearchType = type,
                        Rating_Down = (type == VideoSearchType.Videos ?
                        db.Get<Rating>().Where(x => x.Ratingtype == Ratingtype.Down && x.VideoData_Id == id).ExecuteCount() : db.Get<Rating>().Where(x => x.Ratingtype == Ratingtype.Down && x.Category_Id == id).ExecuteCount()).ConvertValue<long?>().RoundAndFormat(),
                        Rating_Up = (type == VideoSearchType.Videos ?
                        db.Get<Rating>().Where(x => x.Ratingtype == Ratingtype.Up && x.VideoData_Id == id).ExecuteCount() : db.Get<Rating>().Where(x => x.Ratingtype == Ratingtype.Up && x.Category_Id == id).ExecuteCount()).ConvertValue<long?>().RoundAndFormat()
                    });
                }

                return result;
            }

        }


        [HttpGet]
        public void Vote(VideoSearchType type, Ratingtype ratingtype, long id, long userid)
        {
            using (var db = new DbRepository())
            {

                if (type == VideoSearchType.Videos)
                {
                    var video = db.Get<VideoData>().Where(x => x.EntityId == id).ExecuteFirstOrDefault();
                    db.Get<Rating>().Where(x => x.VideoData_Id == id && x.User_Id == userid).Remove();
                    var rating = new Rating();
                    if (video.Rating == null)
                        video.Rating = new List<Rating>();
                    video.Rating.Add(new Rating()
                    {
                        Ratingtype = ratingtype,
                        User_Id = userid,
                    });
                    db.Save(video).SaveChanges();
                }
                else
                {
                    var video = db.Get<VideoCategory>().Where(x => x.EntityId == id).ExecuteFirstOrDefault();
                    db.Get<Rating>().Where(x => x.Category_Id == id && x.User_Id == userid).Remove();
                    var rating = new Rating();
                    if (video.Rating == null)
                        video.Rating = new List<Rating>();
                    video.Rating.Add(new Rating()
                    {
                        Ratingtype = ratingtype,
                        User_Id = userid,
                    });
                    db.Save(video).SaveChanges();
                }
            }
        }

        [HttpGet]
        public ApplicationSettings GetSetting(string key)
        {
            using (var db = new DbRepository())
                return db.Get<ApplicationSettings>().Where(x => x.Key.Contains(key)).ExecuteFirstOrDefault();
        }

        [HttpGet]
        public List<ProductLink> GetProductLinks(string product_Id = null)
        {
            using (var db = new DbRepository())
                return db.Get<ProductLink>().Where(x => string.IsNullOrEmpty(product_Id) || x.Product_Id == product_Id).Execute();
        }

        [HttpGet]
        public string GetPath()
        {
            return Actions.ImageRootPath;
        }

        [HttpPost]
        public void AddLog(string userEmail, string errorMessage)
        {
            if (userEmail != null && errorMessage != null && errorMessage.Length >= 4)
            {
                errorMessage = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(errorMessage));
                using (var db = new DbRepository())
                {
                    var user = db.Get<User>().Where(x => x.Email.Contains(userEmail)).ExecuteFirstOrDefault();
                    var date = DateTime.Now.AddMonths(-3);
                    db.Get<UserErrorLog>().Where(x => x.User_Id == user.EntityId && x.Added <= date).Remove(); /// Remove 3 month old logs for the current user
                    var log = new UserErrorLog()
                    {
                        Added = DateTime.Now,
                        Text = errorMessage,
                        User_Id = user.EntityId.Value
                    };
                    db.Save(log).SaveChanges();
                }
            }
        }
    }
}
