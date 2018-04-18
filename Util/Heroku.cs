using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Todo.net.Util
{
    public  class Heroku
    {
        public static async Task herokuAsync()
        {



            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("Accept: application/vnd.heroku+json; version = 3"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, 
                "https://api.heroku.com/apps/todo-net/config-vars");
            

            await client.SendAsync(request)
                  .ContinueWith(responseTask =>
                  {
                      Console.Write(responseTask.Result);
                      Confgs = responseTask.Result.ToString();
                  });
        }
        public static string Confgs { get; set; }

    }
}
