using System;
using Xamarin.Auth;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.Models
{
    // Static class in shared project to manage useful OAuth references
    public static class GoogleOAuthManager
    {
        private const string CLIENT_ID = Actions.AndroidClientId + ".apps.googleusercontent.com";
        private const string AUTH_SCOPE = "openid email"; // Or whatever you want
        private const string AUTH_URI = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string ACCESSTOKEN_URI = "https://www.googleapis.com/oauth2/v4/token";

        public const string
            REDIRECT_SCHEME = "com.googleusercontent.apps." + Actions.AndroidClientId; // e.g. com.example.myapp

        public const string REDIRECT_PATH = "/oauth2redirect"; // You can change this too

        public static readonly OAuth2Authenticator Authenticator;

        static GoogleOAuthManager()
        {
            Authenticator = new OAuth2Authenticator(
                CLIENT_ID,
                AUTH_SCOPE,
                new Uri(AUTH_URI),
                new Uri(REDIRECT_SCHEME + ":" + REDIRECT_PATH),
                isUsingNativeUI: true)
            {
                AllowCancel = true,
                IsLoadableRedirectUri = true
            };

            Authenticator.AccessTokenUrl = new Uri(ACCESSTOKEN_URI);
        }
    }
}