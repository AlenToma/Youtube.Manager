using System.Web.Http;
using Youtube.Manager.API.Controllers.BaseController;
using Youtube.Manager.API.Models;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.API.Controllers
{
    public class AudienceController : AdminAPController
    {
        [HttpGet]
        [AllowAnonymous]
        public Audience CreateAudience(string applicationName)
        {
            return AudiencesStore.AddAudience(applicationName);
        }


    }
}