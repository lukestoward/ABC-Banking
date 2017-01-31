using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ATM
{
    internal class CustomHttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ResponseBody { get; set; }
    }
}