using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace Youtube.Manager.Core
{
    public class JsonNetResult : JsonResult<object>
    {
        public JsonNetResult(object content, JsonSerializerSettings serializerSettings, Encoding encoding,
            HttpRequestMessage request) : base(content, serializerSettings, encoding, request)
        {
        }


        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = Request.CreateResponse();

            response.Content = new StringContent(JsonConvert.SerializeObject(Content));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Task.FromResult(response);
        }
    }
}