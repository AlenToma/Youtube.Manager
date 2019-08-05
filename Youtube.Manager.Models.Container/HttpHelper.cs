using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Youtube.Manager.Models.Container.Attributes;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using EntityWorker.Core.Helper;
using Youtube.Manager.Models.Container.DB_models.API;
using FastDeepCloner;

namespace Youtube.Manager.Models.Container
{
    public static class HttpHelper
    {
        private static readonly HttpClient client;

        public static string BaseUrl = "http://youtubemanager.ddns.net/Youtube.Manager.API";
        static HttpHelper()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) =>
                {
                    //Debug.WriteLine(cert.GetSerialNumberString());
                    //Debug.WriteLine(cert.Issuer);
                    //Debug.WriteLine(cert.Subject);
                    return true;
                };
                var clientcert = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual
                };
                client = new HttpClient(clientcert);

                //client.BaseAddress = new Uri(Methods.BaseUrl);
            }
            catch (Exception e)
            {
                // 
            }
        }
        private static readonly SafeValueType<string, MethodInformation> cachedMethodInformation = new SafeValueType<string, MethodInformation>();
        public static MethodInformation GetInfo<T, P>(this Expression<Func<T, P>> expression, bool skipArgs = false)
        {
            MethodCallExpression callExpression = expression.Body as MethodCallExpression;
            var method = callExpression.Method;
            var argument = callExpression.Arguments;
            var key = typeof(T).ToString() + typeof(P).ToString() + method.Name + method.ReflectedType.FullName;
            var cached = cachedMethodInformation.ContainsKey(key);
            var item = cachedMethodInformation.GetOrAdd(key, new MethodInformation());
            if (!cached)
            {
                item.IsVoid = method.ReturnType == typeof(void) || method.ReturnType == typeof(Task);
                if (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                    item.CleanReturnType = method.ReturnType.GetActualType();
                else item.CleanReturnType = method.ReturnType;
            }
            var index = 0;
            item.Arguments.Clear();
            if (!skipArgs)
                foreach (var pr in method.GetParameters())
                {
                    var arg = argument[index];
                    var value = arg is ConstantExpression constExp ? constExp.Value : Expression.Lambda(arg).Compile().DynamicInvoke();
                    item.Arguments.Add(pr.Name, value);
                    index++;
                }

            if (!cached)
            {
                var mRoute = method.GetCustomAttribute<Route>();
                var classRoute = typeof(T).GetCustomAttribute<Route>()?.Url ?? "";
                var controller = typeof(T).Name.Substring(1).Replace("Controller", "");
                item.FullUrl = Path.Combine(BaseUrl, classRoute, controller, (mRoute != null && !string.IsNullOrEmpty(mRoute.Url) ? mRoute.Url : method.Name)).Replace("\\", "/");
                item.HttpMethod = mRoute?.HttpMethod ?? HttpMethod.GET;
            }
            return item;

        }


        public static async Task<P> ExecuteAsync<T, P>(Expression<Func<T, P>> expression)
        {
            object result = null;
            try
            {
                MethodInformation item = expression.GetInfo();
                switch (item?.HttpMethod ?? HttpMethod.GET)
                {
                    case HttpMethod.GET:
                        result = await GetAsync(item.FullUrl, item.Arguments, item.IsVoid ? null : item.CleanReturnType);
                        break;

                    case HttpMethod.POST:
                        result = await PostAsync(item.FullUrl, item.Arguments, item.IsVoid ? null : item.CleanReturnType);
                        break;

                    case HttpMethod.JSONPOST:
                        result = await PostAsJsonAsync(item.FullUrl, item.Arguments, item.IsVoid ? null : item.CleanReturnType);
                        break;
                }

                if (typeof(P).IsGenericType && typeof(P).GetGenericTypeDefinition() == typeof(Task<>))
                    result = new DynamicTaskCompletionSource(result, item.CleanReturnType).Task;
                else if (item.IsVoid) result = Task.CompletedTask;


                return (P)result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static async Task<T> PostAsJsonAsync<T>(this string url, object parameter)
        {
            return (T)await (PostAsJsonAsync(url, parameter, typeof(T)));
        }

        public static async Task<T> GetTAsync<T>(this string url, object parameter = null)
        {
            return (T)await (GetAsync(url, parameter, typeof(T)));
        }

        public static async Task<T> PostAsync<T>(this string url, object parameter)
        {
            return (T)await (PostAsync(url, parameter, typeof(T)));
        }


        public static async Task<object> PostAsync(this string url, object parameter, Type castToType = null)
        {
            if (parameter == null)
                throw new Exception("POST operation need a parameters");
            var values = new Dictionary<string, string>();
            if (parameter is Dictionary<string, object>)
                values = (parameter as Dictionary<string, object>).ToDictionary(x => x.Key, x => x.Value?.ToString());
            else
            {
                values = parameter.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(parameter)?.ToString());
            }

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            var contents = await response.Content.ReadAsStringAsync();
            if (castToType != null && !string.IsNullOrEmpty(contents))
                return JsonConvert.DeserializeObject(contents, castToType);
            return null;
        }

        /// <summary>
        /// Post as json.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <param name="castToType"></param>
        /// <returns></returns>
        public static async Task<object> PostAsJsonAsync(this string url, object parameter, Type castToType = null)
        {
            if (parameter == null)
                throw new Exception("POST operation need a parameters");
            var item = parameter is IDictionary<string, object> ? ((IDictionary<string, object>)parameter).Values.FirstOrDefault() : parameter;
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");


            var response = await client.PostAsync(new Uri(url), contentPost);
            var contents = await response.Content.ReadAsStringAsync();
            if (castToType != null && !string.IsNullOrEmpty(contents))
                return JsonConvert.DeserializeObject(contents, castToType);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="castToType">The return json should be converted to </param>
        /// <returns></returns>
        public static async Task<object> GetAsync(this string url, object parameter = null, Type castToType = null)
        {
            if (parameter is IDictionary)
            {
                if (parameter != null)
                {
                    url += "?" + string.Join("&", (parameter as Dictionary<string, object>).Select(x => $"{x.Key}={x.Value ?? ""}"));
                }
            }
            else
            {
                var props = parameter?.GetType().GetProperties();
                if (props != null)
                    url += "?" + string.Join("&", props.Select(x => $"{x.Name}={x.GetValue(parameter)}"));
            }

            var responseString = await client.GetStringAsync(new Uri(url));
            if (castToType != null)
            {
                if (!string.IsNullOrEmpty(responseString))
                    return JsonConvert.DeserializeObject(responseString, castToType);
            }

            return null;
        }

        public static byte[] GetImage(string filename)
        {

            using (var webClient = new WebClient())
            {
                using (var stream = new MemoryStream())
                {
                    webClient.OpenRead(filename).CopyTo(stream);
                    return stream.ToArray();
                }
            }
        }

        public static string UrlDecode(string url)
        {
            return WebUtility.UrlDecode(url);
        }
    }
}
