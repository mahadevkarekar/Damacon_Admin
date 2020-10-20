using Damacon.DAL.Database.EF;
using System.Web;

namespace Damacon.SharedWeb.Helpers
{
    public static class UserManager
    {
        public static void SetLoggedInUser(UserLite user)
        {
            HttpContext.Current.Session["LoggedInUser"] = user;
        }

        public static UserLite GetLoggedInUser()
        {
            if (IsUserLoggedIn())
            {
                return (UserLite)HttpContext.Current.Session["LoggedInUser"];
            }
            else
            {
                return null;
            }
        }

        public static bool IsUserLoggedIn()
        {
            return HttpContext.Current.Session["LoggedInUser"] != null;
        }
    }
}