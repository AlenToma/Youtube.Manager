using System.IO;
using System.Web.Mvc;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.API.Controllers
{
    public class ImagesController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetImage(string imagePath)
        {
            var path = Path.Combine(Actions.ImageRootPath, imagePath);
            if (!System.IO.File.Exists(path))
                return new EmptyResult();

            //var bytes = System.IO.File.OpenRead(path);
            return new FileContentResult(System.IO.File.ReadAllBytes(path), "image/png");
            //return File(bytes, "image/png");
        }
    }
}