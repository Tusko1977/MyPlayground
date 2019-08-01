using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyPlayground.Extensions;

namespace MyPlayground.ApiRequests
{
    public class PatchRequest
    {
        private string _server;
        private string _token;

        public string statusCode;
        public string res;
        public PatchRequest()
        {
            _server = ConfigurationManager.AppSettings["server"];
            _token = ConfigurationManager.AppSettings["token"];
        }

        //Using HttpClientExtensions to create the patch

        public void SendRequest(string body, string InvDetId)
        {
            string _url = $"{_server}/Accounting/qa/Transaction/AdditionalInvDetail/{InvDetId}";

            try
            {
                var client = new HttpClient { BaseAddress = new Uri(_url) };

                client.DefaultRequestHeaders.Add("Authorization", _token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(body, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage message = client.PatchAsync(_url, content).Result;

                res = message.Content.ReadAsStringAsync().Result;

                statusCode = message.StatusCode.ToString();

                return;
            }
            catch (Exception e)
            {
                if (e != null)
                {

                }
            }
        }
    }
}
