namespace Youtube.Manager.Models.Container.DB_models
{
    public class ApiControllerMapping
    {
        public static readonly ApiControllerMapping BaseUrl = new ApiControllerMapping("http://youtubemanager.ddns.net/Youtube.Manager.API/");
        public static readonly ApiControllerMapping API = new ApiControllerMapping(BaseUrl, "api/");
        public static readonly ApiControllerMapping Search = new ApiControllerMapping(API, "Youtube/get");// Get{ searchString , relatedTo }
        public static readonly ApiControllerMapping StreamVideo = new ApiControllerMapping(API, "Youtube/StreamVideo");// Get{ id, formatCode }
        public static readonly ApiControllerMapping StreamHandShake = new ApiControllerMapping(API, "Youtube/StreamHandShake");// Get{ id, formatCode }
        public static readonly ApiControllerMapping GetVideos = new ApiControllerMapping(API, "Youtube/GetVideos");//Get { id }
        public static readonly ApiControllerMapping GetPlaylistItems = new ApiControllerMapping(API, "Youtube/GetPlaylistItems");// Get{ id }
        public static readonly ApiControllerMapping YPlayerUrl = new ApiControllerMapping(API, "Default/Index");// Get { id }
        public static readonly ApiControllerMapping LogIn = new ApiControllerMapping(HttpMethod.POST, API, "Default/LogIn");// Post {email, password} // login or get user
        public static readonly ApiControllerMapping Vote = new ApiControllerMapping(HttpMethod.POST, API, "Default/Vote");// Post {VideoSearchType type, bool up, bool down, long id}
        public static readonly ApiControllerMapping GetVideoCategory = new ApiControllerMapping(HttpMethod.POST, API, "Default/GetVideoCategory");// Post {long userId, long? category_Id}
        public static readonly ApiControllerMapping SaveCategory = new ApiControllerMapping(HttpMethod.POST, API, "Default/SaveCategory");// Post { List<VideoCategory> videoCategories }
        public static readonly ApiControllerMapping GoogleRedirectUrl = new ApiControllerMapping(HttpMethod.POST, API, "Default/Google");
        public static readonly ApiControllerMapping GoogleUserInfo = new ApiControllerMapping("https://www.googleapis.com/oauth2/v2/userinfo");
        public static readonly ApiControllerMapping GetVideoData = new ApiControllerMapping(HttpMethod.POST, API, "Default/GetVideoData");// Post {string _video_Id, long? category_Id = null, long? userId = null, int page = 1}
        public static readonly ApiControllerMapping SaveVideo = new ApiControllerMapping(HttpMethod.POST, API, "Default/SaveVideo");// Post { VideoData video }
        public static readonly ApiControllerMapping ImageUri = new ApiControllerMapping(BaseUrl, "Images/GetImage?imagePath=");// Get{ imagePath }
        public static readonly ApiControllerMapping CreateAudience = new ApiControllerMapping(HttpMethod.GET, API, "Audience/CreateAudience");// Post{ applicationName }
        public static readonly ApiControllerMapping CreateTicket = new ApiControllerMapping(HttpMethod.POST,BaseUrl, "oauth2/token");// Post{ client_Id, grant_type="passowrd", username, password }

        private ApiControllerMapping(params string[] values)
        {
            Value = string.Join("", values);
            HttpMethod = HttpMethod.GET;
        }

        private ApiControllerMapping(HttpMethod method = HttpMethod.GET, params string[] values)
        {
            Value = string.Join("", values);
            HttpMethod = method;
        }

        public HttpMethod HttpMethod { get; private set; }

        private string Value;

        public static implicit operator string(ApiControllerMapping f)
        {
            return f.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
