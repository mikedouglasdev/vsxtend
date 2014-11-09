using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vsxtend.Samples.Mvc.Models;
using Vsxtend.Common;
using Vsxtend.Entities;
using Vsxtend.Interfaces;
using Vsxtend.Samples.Mvc.ViewModels;

namespace Vsxtend.Samples.Mvc.Controllers
{
    public class OAuthController : ControllerBase
    {
        private readonly IOAuthUtility OAuthUtility;
        public OAuthController(IOAuthUtility oauthUtility)
            : base(oauthUtility)
        {
            this.OAuthUtility = oauthUtility;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestToken(string code, string status)
        {
            string state = Request.UrlReferrer.Query.Replace("?ReturnUrl=", "");
            string authorizationUrl = ConfigurationManager.AppSettings["AuthUrl"];
            string applicationId = ConfigurationManager.AppSettings["AppId"];
            string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];

            return new RedirectResult(OAuthUtility.GenerateAuthorizeUrl(authorizationUrl, applicationId, redirectUrl, state));
        }

        public async Task<ActionResult> Renew()
        {
            OAuthToken token = null;
            string vsoTokenRaw = GetVsoTokenStringOrDefault();

            if (!string.IsNullOrEmpty(vsoTokenRaw))
            {
                token = JsonConvert.DeserializeObject<OAuthToken>(vsoTokenRaw);
            }

            string tokenUrl = ConfigurationManager.AppSettings["TokenUrl"];
            string applicationSecret = ConfigurationManager.AppSettings["AppSecret"];
            string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];

            OAuthToken oauthToken = await OAuthUtility.GetOAuthToken(tokenUrl, applicationSecret, token.refreshToken, redirectUrl, true);

            if (!string.IsNullOrEmpty(oauthToken.Error))
            {
                ViewBag.Token = token;
                return View("TokenView");
            }

            // Save back to database
            SaveOauthTokenToDatabase(oauthToken);

            string returnUrl = Request.QueryString["ReturnUrl"];

            return RedirectToLocal(returnUrl);
  
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }

        public async Task<ActionResult> Authorize(string code = "", string state = "")
        {

            //return RedirectToAction("NotAuthorized");


            if (Request["error"] != null && Request["error"] == "access_denied")
            {
                return RedirectToAction("NotAuthorized");
            }

            string tokenUrl = ConfigurationManager.AppSettings["TokenUrl"];
            string applicationSecret = ConfigurationManager.AppSettings["AppSecret"];
            string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];

            OAuthToken oauthToken = await OAuthUtility.GetOAuthToken(tokenUrl, applicationSecret, code, redirectUrl, false);

            // Save to cookie
            SaveOauthTokenToDatabase(oauthToken);

            string returnUrl = Request.QueryString["state"];

            return RedirectToLocal(returnUrl);
        }

        private void SaveOauthTokenToDatabase(OAuthToken oauthToken)
        {

            string userId = string.Empty;
            var dbContext = new TFSRESTClientContext();
            string oauthTokenSerialized = JsonConvert.SerializeObject(oauthToken);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("VsoUserGuid"))
            {
                userId = this.ControllerContext.HttpContext.Request.Cookies["VsoUserGuid"].Value;
                var user = dbContext.Users.SingleOrDefault(u => u.Uid == userId);
                user.VsoToken = oauthTokenSerialized;
            }
            else
            {
                userId = Guid.NewGuid().ToString();
                HttpCookie cookie = new HttpCookie("VsoUserGuid");
                cookie.Value = userId;
                cookie.Expires = DateTime.Now.AddYears(1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);


                dbContext.Users.Add(new Entities.User { Uid = userId, VsoToken = oauthTokenSerialized });
            }

            dbContext.SaveChanges();
            
            Session["VsoTokenTimeout"] = DateTime.Now.AddMinutes(13);
        }

        private string GetVsoTokenStringOrDefault()
        {
            string userId = string.Empty;
            var dbContext = new TFSRESTClientContext();

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("VsoUserGuid"))
            {
                userId = this.ControllerContext.HttpContext.Request.Cookies["VsoUserGuid"].Value;
                var user = dbContext.Users.SingleOrDefault(u => u.Uid == userId);
                return user.VsoToken;
            }

            return null;
        }
    }
}
