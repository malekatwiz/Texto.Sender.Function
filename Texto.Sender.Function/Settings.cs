using System.Configuration;

namespace Texto.Sender.Function
{
    public static class Settings
    {
        public static string SkybotAuthUri => ConfigurationManager.AppSettings["SkybotAuthUri"];
        public static string TextoApiUri => ConfigurationManager.AppSettings["TextApi"];
        public static string ClientId => ConfigurationManager.AppSettings["ClientId"];
        public static string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
    }
}
