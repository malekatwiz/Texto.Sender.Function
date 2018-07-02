using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Texto.Sender.Function
{
    public class TextoClient
    {
        private readonly HttpClient client;
        private string token;

        public TextoClient()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Send(TextMessage textMessage)
        {
            if (string.IsNullOrEmpty(token))
            {
                await GetToken();
            }

            var queryObject = new
            {
                FromNumber = textMessage.From,
                ToNumber = textMessage.To,
                Message = textMessage.Body
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            await client.PostAsJsonAsync($"{Settings.TextoApiUri}/api/text/Send", queryObject);
        }

        private async Task GetToken()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"clientId", Settings.TextoClientId},
                {"clientSecret", Settings.TextoClientSecret },
                {"identifier", Settings.TextoClientIdentifier }
            });

            var response = await client.PostAsync($"{Settings.TextoApiUri}/api/text/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            token = JsonConvert.DeserializeObject<string>(responseContent);
        }
    }
}
