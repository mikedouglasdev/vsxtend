using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vsxtend.Samples.Mvc.Filters;
using Vsxtend.Common;
using Vsxtend.Entities;
using Vsxtend.Interfaces;
using Vsxtend.Resources;

namespace Vsxtend.Samples.Mvc.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(IOAuthUtility oauthUtility, ITeamRoomClient teamRoomClient) : base(oauthUtility)
        {
            this.OAuthUtility = oauthUtility;
            this.TeamRoomClient = teamRoomClient;
        }

        private ITeamRoomClient TeamRoomClient;


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
                
            return View();
        }

        [VsoAuthorize]
        public async Task<ActionResult> OAuthSample()
        {
            var result = await TeamRoomClient.GetRoomsAsync();
            return View(result);   
        }

        public async Task<ActionResult> OAuthSampleNoFilter()
        {
            string code = "";

            if (Request.Cookies.AllKeys.Contains("VsoUserGuid"))
            {
                code = Request.Cookies["VsoUserGuid"].Value;
            }

            if (string.IsNullOrEmpty(code))
            {
                RedirectToAction("index", "oauth", new { ReturnUrl = Request.Url.LocalPath });
            }
            // Is the Authorize token still valid, if not redirect to the renew action
            else if (Session == null || Session["VsoTokenTimeout"] == null 
                || DateTime.Parse(Session["VsoTokenTimeout"].ToString()) <= DateTime.Now)
            {
                RedirectToAction("renew", "oauth", new { ReturnUrl = Request.Url.LocalPath });
            }

            var result = await TeamRoomClient.GetRoomsAsync();
            return View("OAuthSample", result);   
        }
    }
}
