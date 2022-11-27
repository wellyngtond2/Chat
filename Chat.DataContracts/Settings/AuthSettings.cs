namespace Chat.DataContracts.Settings
{
    public class AuthSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
