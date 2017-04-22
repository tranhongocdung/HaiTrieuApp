using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWeb.Libraries
{
    public class Utility
    {
        public static void SetSession(string sessionName, object sessionValue)
        {
            HttpContext.Current.Session[sessionName] = sessionValue;
        }
        public static void RemoveSession(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
        public static object GetSession(string sessionName)
        {
            return HttpContext.Current.Session[sessionName];
        }
    }
}