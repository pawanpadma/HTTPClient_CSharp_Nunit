using HTTPClient_CSharp_Nunit.Config;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient_CSharp_Nunit.Common_HTTP_Methods
{
    public class HTTPMethods
    {
        public ReadEnvType read;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // public Login login;

        public string token;
        public string randomname = Path.GetRandomFileName().Replace(".", "");

        public HTTPMethods()
        {
            read = new ReadEnvType();
        }

        public async Task<HttpResponseMessage> PostMethod(string endPoint, StringContent payLoad, string token)
        {
            var baseUrl = read.GetProperty("base", "baseurl");
            var url = baseUrl + endPoint;
            log.Info("url is " + url);
            var TaskList = new List<string>();
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                // client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.BaseAddress = new Uri(baseUrl);

                var result = await client.PostAsync(url, payLoad);

                return result;
            }
        }

        public async Task<HttpResponseMessage> GetMethod(string endPoint, String token)
        {
            var baseUrl = read.GetProperty("base", "baseurl");
            var url = baseUrl + endPoint;

            var TaskList = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.GetAsync(url);

                return response;
            }
        }
    }
}