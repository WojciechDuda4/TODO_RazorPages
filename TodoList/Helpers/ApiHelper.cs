using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TodoList.Helpers
{
    public class ApiHelper
    {
        public HttpClient ApiClient { get; set; }

        public ApiHelper()
        {
            InitializeClient();
        }

        void InitializeClient()
        {
            ApiClient = new HttpClient();
            //ApiClient.BaseAddress = new Uri("");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
