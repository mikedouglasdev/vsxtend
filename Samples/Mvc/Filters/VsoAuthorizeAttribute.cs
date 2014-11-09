using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Vsxtend.Samples.Mvc.Filters
{
    public class VsoAuthorizeAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string code = "";

            if (filterContext.HttpContext.Request.Cookies.AllKeys.Contains("VsoUserGuid"))
            {
                code = filterContext.HttpContext.Request.Cookies["VsoUserGuid"].Value;
            }

            if (string.IsNullOrEmpty(code))
            {
                var controller = (Vsxtend.Samples.Mvc.Controllers.ControllerBase)filterContext.Controller;
                filterContext.Result = controller.RedirectToAction("index", "oauth", new { ReturnUrl = filterContext.HttpContext.Request.Url.LocalPath });
            }
            // Is the Authorize token still valid, if not redirect to the renew action
            else if (filterContext.HttpContext.Session == null || filterContext.HttpContext.Session["VsoTokenTimeout"] == null 
                || DateTime.Parse(filterContext.HttpContext.Session["VsoTokenTimeout"].ToString()) <= DateTime.Now)
            {
                var controller = (Vsxtend.Samples.Mvc.Controllers.ControllerBase)filterContext.Controller;
                filterContext.Result = controller.RedirectToAction("renew", "oauth", new { ReturnUrl = filterContext.HttpContext.Request.Url.LocalPath });
            }
        }
    }
}