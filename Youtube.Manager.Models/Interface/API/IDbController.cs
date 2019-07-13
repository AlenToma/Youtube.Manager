using System.Collections.Generic;
using System.Threading.Tasks;
using Youtube.Manager.Models.Container.Attributes;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;

namespace Youtube.Manager.Models.Container.Interface.API
{
    [Route(url: "api/")]
    public interface IDbController
    {
        List<ProductLink> GetProductLinks(string product_Id = null);
        ApplicationSettings GetSetting(string key);
        User LogIn(string email, string imageUrl, string password);
        Task Vote(VideoSearchType type, Ratingtype ratingtype, long id, long userid);
        List<RatingModelView> GetItemRating(VideoSearchType type, List<long> ids);
        List<VideoCategoryView> GetVideoCategory(long? userId = null, long? categoryId = null);
        Task<List<VideoData>> GetVideoData(string videoId, long? categoryId = null, long? userId = null, int page = 1);
        [Route(httpMethod: HttpMethod.JSONPOST)]
        Task SaveVideo(VideoData video);
        [Route(httpMethod: HttpMethod.JSONPOST)]
        Task<long> SaveCategory(VideoCategory videoCategory);
        List<VideoCategoryView> GetUserSuggestion(long userId);
        [Route(httpMethod: HttpMethod.JSONPOST)]
        User SaveUser(User user);
    }
}
