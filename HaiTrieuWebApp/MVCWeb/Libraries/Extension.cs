using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using MVCWeb.AppDataLayer;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Libraries
{
    public static class Extension
    {
        public static string GetLast(this string source, int tailLength)
        {
            return tailLength >= source.Length ? source : source.Substring(source.Length - tailLength);
        }
        public static void CopyPropertiesTo(this object source, object destination)
        {
            PropertyInfo[] myObjectFields = source.GetType().GetProperties(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(o=>!o.GetMethod.IsVirtual).ToArray();

            foreach (PropertyInfo fi in myObjectFields)
            {
                fi.SetValue(destination, fi.GetValue(source));
            }
        }
        public static ReturnLabel ToReturnLabel(this Customer source, bool full = true)
        {
            var label = new ReturnLabel
            {
                Id = source.Id.ToString(),
                Label = source.CustomerName +
                        (!string.IsNullOrEmpty(source.PhoneNo) ? " - " + source.PhoneNo : "") +
                        (!string.IsNullOrEmpty(source.Email) ? " - " + source.Email : "")
            };
            if (full)
            {
                label.Label +=
                    (!string.IsNullOrEmpty(source.District) ? " - " + source.District : "") +
                    (!string.IsNullOrEmpty(source.City) ? " - " + source.City : "");
            }
            return label;
        }
    }
}