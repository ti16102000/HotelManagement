using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace HotelManagement
{
    public class GlobalVariables
    {
        public static HttpClient client = new HttpClient();
        static GlobalVariables()
        {
            client.BaseAddress = new Uri("http://localhost:53561/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}