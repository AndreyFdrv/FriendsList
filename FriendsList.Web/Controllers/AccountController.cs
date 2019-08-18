using System;
using System.Web.Mvc;

namespace FriendsList.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            string returnUrl = String.Format("https://oauth.vk.com/authorize?client_id=7100382&redirect_uri={0}&" +
                "response_type=code&scope=friends", 
                Url.Action("Index", "Home", null, Request.Url.Scheme));
            return Redirect(returnUrl);
        }
    }
}