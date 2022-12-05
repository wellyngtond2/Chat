using System.Security.Cryptography;
using System.Text;

namespace Chat.Share.Helpers
{
    public static class SecurityHelper
    {
        public static string StringToHash(string input)
        {
            if (input is null) return input;

            byte[] data = Encoding.ASCII.GetBytes(input);
            data = new SHA256Managed().ComputeHash(data);
            var hash = Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}
