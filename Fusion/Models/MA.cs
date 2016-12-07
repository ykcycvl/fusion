using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.DirectoryServices;

namespace Fusion.Controllers
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private bool IsUserInGroup(string groupName, string userName)
        {
            bool res = false;
            using (DirectorySearcher ds = new DirectorySearcher("SAMAccountName=" + userName))
            {
                SearchResult sr = ds.FindOne();
                if (sr == null) return false;
                using (DirectoryEntry user = sr.GetDirectoryEntry())
                {
                    ds.Filter = "(&(Name=" + groupName + ")(objectClass=group))";
                    sr = ds.FindOne();
                    if (sr == null) return false;
                    using (DirectoryEntry group = sr.GetDirectoryEntry())
                    {
                        if (!(bool)group.Invoke("IsMember", new object[] { user.Path }))
                            return false;
                    }
                    res = true;
                }
            }
            return res;
        }

        private string[] allowedUsers = new string[] { };
        private string[] allowedRoles = new string[] { };

        public MyAuthorizeAttribute()
        { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Users))
            {
                allowedUsers = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < allowedUsers.Length; i++)
                {
                    allowedUsers[i] = allowedUsers[i].Trim();
                }
            }
            if (!String.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    allowedRoles[i] = allowedRoles[i].Trim();
                }
            }

            return httpContext.Request.IsAuthenticated &&
                 User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsers.Length > 0)
            {
                return allowedUsers.Contains(httpContext.User.Identity.Name);
            }
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (allowedRoles.Length > 0)
            {
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (IsUserInGroup(allowedRoles[i], httpContext.User.Identity.Name))
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}