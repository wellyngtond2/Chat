using Chat.Share.Events.Interfaces;

namespace Chat.Share.Events
{
    public class MessageEvent<T> where T : IEvent
    {
        public T Data { get; private set; }

        public void SetData(T data)
        {
            Data = data;
        }
    }
}
