using Rest.API.Translator;
using System.Collections.Generic;
using System.Threading.Tasks;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;

namespace Realm.Of.Y.Manager.Models.Container.Interface.API
{
    [Route(relativeUrl: "api")]
    public interface IDbController
    {
        List<ProductLink> GetProductLinks(string product_Id = null);
        ApplicationSettings GetSetting(string key);
        User LogIn(string email, string imageUrl, string password);
        Task Vote(VideoSearchType type, Ratingtype ratingtype, long id, long userid);
        List<RatingModelView> GetItemRating(VideoSearchType type, List<long> ids);
        List<VideoCategoryView> GetVideoCategory(long? userId = null, long? categoryId = null);
        Task<List<VideoData>> GetVideoData(string videoId, long? categoryId = null, long? userId = null, int page = 1);
        List<VideoCategoryView> GetUserSuggestion(long userId);

        [Route(httpMethod: MethodType.JSONPOST)]
        Task SaveVideo(VideoData video);

        [Route(httpMethod: MethodType.JSONPOST)]
        Task<long> SaveCategory(VideoCategory videoCategory);

        [Route(httpMethod: MethodType.JSONPOST)]
        User SaveUser(User user);

        [Route(httpMethod: MethodType.POST)]
        Task AddLog(string userEmail, string errorMessage);
    }
}
