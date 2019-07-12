using System.Web.Http;
using Youtube.Manager.API.Models.Attributes;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.API.Controllers.BaseController
{
    [PAuthorize(Roles.Admin)]
    public class AdminAPController : ApiController
    {
    }
}