using System.Text.RegularExpressions;

namespace Sample.Api.Helpers
{
    public static class StringHelper
    {
        public static string Humanize(string strIn)
        {
            return Regex.Replace(strIn, "([A-Z]{1,2}|[0-9]+)", " $1").TrimStart();
        }
    }
}