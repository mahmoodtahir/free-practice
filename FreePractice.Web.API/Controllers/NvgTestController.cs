using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreePractice.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NvgTestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NvgTestController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("sampleone")]
        public async Task<ActionResult<string>> Get()
        {
            string data = "";

            // await foreach (var item in GetPosts())
            await foreach (var item in GetPromotionData())
            {
                Debug.WriteLine(data);
                data += item;
            }
            return data;
        }

        [HttpGet]
        [Route("sampletwo")]
        public async IAsyncEnumerable<string> GetAsync()
        {
            var httpClient = new HttpClient();
            var resp = await httpClient.GetAsync("https://uks-kc-nvgapi-dev-web.azurewebsites.net//api/v-1.0/tpm/incremental");
            
            yield return await resp.Content.ReadAsStringAsync();
        }


        #region Private
        static HttpClient httpClient = GetHttpClient();
        private static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            // httpClient.BaseAddress = new Uri(@"https://jsonplaceholder.typicode.com/");
            httpClient.BaseAddress = new Uri(@"https://uks-kc-nvgapi-dev-web.azurewebsites.net/");
            return httpClient;
        }

        private static async IAsyncEnumerable<string> GetPosts()
        {
            string jsonString = "";

            for (int i = 1; i < 10; i++)
            {
                string requestEndPoint = $"posts/{i}";

                HttpResponseMessage response = await httpClient.GetAsync(requestEndPoint);
                HttpContent content = response.Content;

                jsonString = await content.ReadAsStringAsync();

                yield return jsonString;
            }

            yield return jsonString;
        }


        private static async IAsyncEnumerable<string> GetPromotionData()
        {
            string jsonString = "";

            for (int i = 0; i < 5; i++)
            {
                IEnumerable<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string> ("pageIndex", i.ToString()),
                    // new KeyValuePair<string, string>("pageSize", "2")
                };

                HttpContent postConent = new FormUrlEncodedContent(data);

                HttpResponseMessage response = await httpClient.PostAsync("api/v-1.0/tpm/incremental", postConent);
                HttpContent content = response.Content;

                jsonString = await content.ReadAsStringAsync();

                yield return jsonString;
            }

            yield return jsonString;
        }

        #endregion

    }
}
