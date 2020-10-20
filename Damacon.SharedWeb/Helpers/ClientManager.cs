using Damacon.DAL.Database.EF;
using System.Web;

namespace Damacon.SharedWeb.Helpers
{
    public static class ClientManager
    {
        public static void SetLoggedInUser(ClientLite user)
        {
            HttpContext.Current.Session["LoggedInClient"] = user;
        }

        public static ClientLite GetLoggedInUser()
        {
            if (IsUserLoggedIn())
            {
                return (ClientLite)HttpContext.Current.Session["LoggedInClient"];
            }
            else
            {
                return null;
            }
        }

        public static bool IsUserLoggedIn()
        {
            return HttpContext.Current.Session["LoggedInClient"] != null;
        }
    }
}