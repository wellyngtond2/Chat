namespace Chat.DataContracts.ChatMessage.Response
{
    public class ChatMessageResponse
    {
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public string Message{ get; set; }
        public DateTime Date { get; set; }

    }
}
