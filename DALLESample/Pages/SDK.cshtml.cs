using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DALLESample.Pages
{
    public class SDKModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string promptText)
        {
            var apikey = "{your api key}";
            var apiUrl = "https://{your openai endpoint}.openai.azure.com";

            var client = new OpenAIClient(new Uri(apiUrl), new AzureKeyCredential(apikey));
            try
            {
                var result = await client.GetImageGenerationsAsync(new ImageGenerationOptions()
                {
                    Prompt = promptText,
                    Size = ImageSize.Size1024x1024,
                    ImageCount = 1
                });

                return Redirect(result.Value.Data.First().Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
