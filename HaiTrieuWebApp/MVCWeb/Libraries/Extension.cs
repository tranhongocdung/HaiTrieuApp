using System;
using System.Linq;
using System.Reflection;

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
    }
}