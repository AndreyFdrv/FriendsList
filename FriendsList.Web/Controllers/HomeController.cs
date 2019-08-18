using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;
using FriendsList.Web.Models;

namespace FriendsList.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string code = null, string error = null, string error_description = null)
        {
            const string apiVersion = "5.101";
            var authorizationInfo = GetJObjectByUrl(String.Format("https://oauth.vk.com/access_token?client_id=7100382&" +
                "client_secret=aiWcYe8ZccCjD941dMZp&redirect_uri={0}&code={1}",
                Url.Action("Index", "Home", null, Request.Url.Scheme), code));
            var accessToken = authorizationInfo["access_token"].ToString();
            var userID = authorizationInfo["user_id"].ToString();

            var model = new UserModel();
            var user = GetJObjectByUrl(String.Format("https://api.vk.com/method/users.get?v={0}&user_id={1}&access_token={2}", 
                apiVersion, userID, accessToken))["response"][0];
            model.FirstName = user["first_name"].ToString();
            model.LastName = user["last_name"].ToString();

            JArray friends = JArray.FromObject(GetJObjectByUrl(String.Format("https://api.vk.com/method/friends.get?v={0}&" +
                "fields=nickname&access_token={1}", 
                apiVersion, accessToken))["response"]["items"]);
            const int maxFriendsCount = 5;
            model.Friends = new List<UserModel>();
            for(int i=0; i<friends.Count; i++)
            {
                if (i >= maxFriendsCount)
                    break;
                var friend = new UserModel()
                {
                    FirstName = friends[i]["first_name"].ToString(),
                    LastName = friends[i]["last_name"].ToString()
                };
                model.Friends.Add(friend);
            }

            return View(model);
        }

        private JObject GetJObjectByUrl(string requestUrl)
        {
            var request = WebRequest.Create(requestUrl);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();

            string responseStr = null;
            using (var stream = response.GetResponseStream())
            {
                var sr = new StreamReader(stream);
                responseStr = sr.ReadToEnd();
                sr.Close();
            }
            return JObject.Parse(responseStr);
        }
    }
}