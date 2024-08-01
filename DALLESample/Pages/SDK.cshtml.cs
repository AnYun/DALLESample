using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenAI.Chat;
using OpenAI.Images;

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

            var client = new AzureOpenAIClient(
                    new Uri(apiUrl), 
                    new AzureKeyCredential(apikey))
                .GetImageClient("dall-e-3");

            try
            {
                ImageGenerationOptions options = new()
                {
                    Quality = GeneratedImageQuality.High,
                    Size = GeneratedImageSize.W1792xH1024,
                    Style = GeneratedImageStyle.Natural,
                    ResponseFormat = GeneratedImageFormat.Uri
                };

                var image = client.GenerateImage(promptText, options);

                return Redirect(image.Value.ImageUri.AbsoluteUri);

                /*
                ImageGenerationOptions options = new()
                {
                    Quality = GeneratedImageQuality.High,
                    Size = GeneratedImageSize.W1792xH1024,
                    Style = GeneratedImageStyle.Natural,
                    ResponseFormat = GeneratedImageFormat.Bytes
                };

                GeneratedImage image = client.GenerateImage(promptText, options);
                BinaryData bytes = image.ImageBytes;

                return File(bytes.ToStream(), "image/png");
                */
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
