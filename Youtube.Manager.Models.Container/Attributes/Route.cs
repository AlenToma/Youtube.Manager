using System;

namespace Youtube.Manager.Models.Container.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public class Route : Attribute
    {
        public readonly string Url;

        public readonly HttpMethod HttpMethod;


        /// <summary>
        /// Empty url mean the method name is the is the url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        public Route(string url = null, HttpMethod httpMethod = HttpMethod.GET)
        {
            Url = url;
            HttpMethod = httpMethod;
        }

    }
}
