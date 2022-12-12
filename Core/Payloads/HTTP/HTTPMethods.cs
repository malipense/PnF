using System.Collections.Generic;

namespace P_n_F.Core.Payloads.HTTP
{
    public static class HTTPMethods
    {
        public static readonly List<string> Methods = new List<string>()
        {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD",
            "OPTIONS",
            "CONNECT",
            "TRACE",
            "PATCH"
        };
    }
}
