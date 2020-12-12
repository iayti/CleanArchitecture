using System.Linq;
using System.Web;

namespace Application.Common.Mapping
{
    public static class StringExtensions
    {
        public static string ParseObjectToQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}
