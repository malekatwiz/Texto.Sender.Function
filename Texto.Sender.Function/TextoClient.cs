using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Texto.Sender.Function
{
    public class TextoClient
    {
        private readonly HttpClient _client;
        private string _token;

        public TextoClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Send(TextMessage textMessage)
        {
            if (string.IsNullOrEmpty(_token))
            {
                await GetToken();
            }

            var queryObject = new
            {
                FromNumber = textMessage.From,
                ToNumber = textMessage.To,
                Message = textMessage.Body
            };

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

            await _client.PostAsJsonAsync($"{Settings.TextoApiUri}/api/text/send", queryObject);
        }

        private async Task GetToken()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"clientId", Settings.ClientId},
                {"clientSecret", Settings.ClientSecret },
                {"grant_type", "client_credentials" }
            });

            var response = await _client.PostAsync($"{Settings.SkybotAuthUri}/connect/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

            _token = deserializedResponse.access_token;
        }
    }
}
