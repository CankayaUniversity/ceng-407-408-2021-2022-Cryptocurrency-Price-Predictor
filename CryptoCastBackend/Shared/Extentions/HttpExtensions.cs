using Microsoft.AspNetCore.Http;

namespace Shared.Extentions
{
    public static class HttpExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQueryString(this IHttpContextAccessor request, string key)
        {
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = request.HttpContext.Request.Query[key].ToString();
            if (queryStrings == null)
                return null;

            var match = request.HttpContext.Request.Query[key].ToString();
            if (string.IsNullOrEmpty(match))
                return null;

            return match;
        }
    }
}
