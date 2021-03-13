using System.Linq;
using System.Web;

namespace CleanArchitecture.Application.Common.Mapping
{
    public static class StringExtensions
    {
        public static string ParseObjectToQueryString(object obj, bool lowercase)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select (lowercase ? p.Name.ToLower() : p.Name) + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}