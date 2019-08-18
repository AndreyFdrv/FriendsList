using System.Collections.Generic;

namespace FriendsList.Web.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<UserModel> Friends { get; set; }
    }
}