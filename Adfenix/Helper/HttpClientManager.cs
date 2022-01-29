using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Helper
{
    public static class HttpClientManager
    {
        /// <summary>
        /// Returns the instance of IHttpClientFactory.
        /// It used to Avoid Socket Exhaustion
        /// </summary>
        public static IHttpClientFactory HttpClientFactory { get; set; }

    }
}
