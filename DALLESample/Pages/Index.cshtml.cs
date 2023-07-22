using DALLESample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DALLESample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string promptText)
        {
            var apikey = "{your api key}";
            var apiUrl = "https://{your openai endpoint}.openai.azure.com/openai/images/generations:submit?api-version=2023-06-01-preview";
            var client = new HttpClient();

            // 1. 先取得 operation-location
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            request.Headers.Add("api-key", apikey);
            var prompt = new
            {
                prompt = promptText,
                n = 1,
                size = "1024x1024"
            };
            var content = new StringContent(JsonSerializer.Serialize(prompt), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();


            var location = response.Headers.GetValues("operation-location").First();

            // 2. 再取得圖片網址
            var result = new DALLEResponse();
            while (result.status != "succeeded" && result.status != "failed")
            {
                Thread.Sleep(1000);
                request = new HttpRequestMessage(HttpMethod.Get, location);
                request.Headers.Add("api-key", apikey);
                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                result = JsonSerializer.Deserialize<DALLEResponse>(await response.Content.ReadAsStringAsync());
            }

            if (result.status == "failed")
                return Content("failed!");

            return Redirect(result.result.data[0].url);
        }
    }
}