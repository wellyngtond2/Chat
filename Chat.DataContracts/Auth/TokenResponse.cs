namespace Chat.DataContracts.Auth
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public int ExpirationIn { get; set; }
    }
}
