using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracking.Models.Repository
{
    public class OAuthRepository
    {
        Dictionary<string, OAuthorizationdto> oAuthRepo = null;
        Controller oAuthcontroller = null;
        public OAuthRepository()
        {
            oAuthRepo = new Dictionary<string, OAuthorizationdto>();
        }
        internal OAuthorizationdto Get(object controller)
        {
            oAuthcontroller = controller as System.Web.Mvc.Controller;
            string secretKey = oAuthcontroller.TempData["secretKey"] as string;
            Dictionary<string, OAuthorizationdto> oAuthRepo = oAuthcontroller.TempData["OAuthorization"] as Dictionary<string, OAuthorizationdto>;
            oAuthcontroller.TempData.Keep();
            return oAuthRepo[secretKey];

        }
        internal bool Save(object controller, OAuthorizationdto oAuthorizationdto)
        {
            oAuthcontroller = controller as System.Web.Mvc.Controller;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string secretKey = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            oAuthcontroller.TempData["secretKey"] = secretKey;
            oAuthRepo.Add(secretKey, oAuthorizationdto);
            oAuthcontroller.TempData["OAuthorization"] = oAuthRepo;
            oAuthcontroller.TempData.Keep();
            return true;
        }
    }
}