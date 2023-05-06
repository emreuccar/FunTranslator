using FunTranslator.Models;

namespace FunTranslator.Business
{
    public class FunTranslatorApi
    {
        public HttpResponseMessage? Translate(string text, string url, string translationType)
        {
            var translateViewModel = new TranslateViewModel
            {
                text = text
            };

            HttpResponseMessage? result;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var postTask = client.PostAsJsonAsync(translationType, translateViewModel);
                postTask.Wait();

                result = postTask.Result;
            }

            return result;
        }
    }
}
