using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vsxtend.Common;
using Vsxtend.Entities;
using Vsxtend.Interfaces;

namespace Vsxtend.Samples.Mvc.Controllers
{
    public class ControllerBase : Controller
    {
        protected IOAuthUtility OAuthUtility { get; set; }

        public ControllerBase (IOAuthUtility oauthUtility)
	    {
                this.OAuthUtility = oauthUtility;
	    }

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }

        public new RedirectToRouteResult RedirectToAction(string action, string controller, object routeValues)
        {
            return base.RedirectToAction(action, controller, routeValues);
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}