using System.Configuration;

namespace Texto.Sender.Function
{
    public static class Settings
    {
        public static string FromNumber => ConfigurationManager.AppSettings["TextNumbersFrom"];
        public static string ToNumber => ConfigurationManager.AppSettings["TextNumbersTo"];
        public static string TextoApiUri => ConfigurationManager.AppSettings["TextApi"];
        public static string TextoClientId => ConfigurationManager.AppSettings["TextoCredentialsClientId"];
        public static string TextoClientSecret => ConfigurationManager.AppSettings["TextoCredentialsClientSecret"];
        public static string TextoClientIdentifier => ConfigurationManager.AppSettings["TextoCredentialsIdentifier"];
    }
}
