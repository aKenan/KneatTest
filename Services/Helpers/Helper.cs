using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Helpers
{
    public class Helper
    {
        /// <summary>
        /// Invokes GET API
        /// </summary>
        /// <param name="url">api url</param>
        /// <returns>json string</returns>
        public async Task<HttpResponseMessage> GetJsonString(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var data = await client.GetAsync(url); //invoke HTTP GET

                return data;
            }
        }

        /// <summary>
        /// Get error message from HttpResponseMessage
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetApiUnsuccessfullMessage(HttpResponseMessage data)
        {
            return $"UNSUCCESSFULL:  Status code: {data.StatusCode}, Message: {data.ReasonPhrase}";
        }
    }
}
