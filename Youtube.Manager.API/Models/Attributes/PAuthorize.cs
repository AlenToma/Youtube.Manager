using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Youtube.Manager.API.Models.Attributes
{
    public class PAuthorize : AuthorizeAttribute
    {
        public PAuthorize()
        {
            base.Roles = Manager.Models.Container.Roles.Admin.ToString();
        }

        public PAuthorize(params Manager.Models.Container.Roles[] roles)
        {
            if (roles != null && roles.Any())
                base.Roles = string.Join(",", roles.Select(x => x.ToString()));
            else
                base.Roles = Manager.Models.Container.Roles.Admin.ToString();
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext ctx)
        {
            base.HandleUnauthorizedRequest(ctx);
        }
    }
}