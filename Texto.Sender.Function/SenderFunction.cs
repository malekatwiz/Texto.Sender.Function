using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Texto.Sender.Function
{
    public static class SenderFunction
    {
        private static readonly TextoClient textoClient;

        static SenderFunction()
        {
            textoClient = new TextoClient();
        }

        [FunctionName("SenderFunction")]
        public static async void RunAsync([ServiceBusTrigger("outgoingtexts", AccessRights.Listen, Connection = "SkysendBusConnectionString")]string queueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {queueItem}");

            await textoClient.Send(ConvertToTextMessage(queueItem));
        }

        private static TextMessage ConvertToTextMessage(string item)
        {
            return JsonConvert.DeserializeObject<TextMessage>(item);
        }
    }
}
