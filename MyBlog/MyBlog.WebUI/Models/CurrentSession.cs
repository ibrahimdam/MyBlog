using MyBlog.Entities.Concrete;
using System.Text.Json;

namespace MyBlog.WebUI.Models
{
    public class CurrentSession
    {
        // Ödev: Current Session class'ını sadece BlogUser nesnesi için yaptık.
        // Bunu Generic hale getirip herhangi bir nesneyi sessiona atacak hale getiriniz.

        static HttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public static BlogUser CurrentUser { 
            get
            { 
                return GetUser("currentUser");    
            } 
        }

        public static BlogUser GetUser(string key)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.Session.GetString(key) != null)
            {
                string currentUserJson = httpContext.Session.GetString(key);
                BlogUser currentUser = JsonSerializer.Deserialize<BlogUser>(currentUserJson);
                return currentUser;
            }
            return null;
        }

        public static void SetUser(string key, BlogUser user)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            string currentUserJson = JsonSerializer.Serialize<BlogUser>(user);
            httpContext.Session.SetString(key, currentUserJson);

        }
        public static void Clear()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            httpContext.Session.Clear();
        }

        public static void Remove(string key)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            httpContext.Session.Remove(key);
        }

    }
}
