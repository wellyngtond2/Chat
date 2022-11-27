namespace Chat.DataContracts.Base
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }

        public static BaseResponse<T> Create(T data) => new BaseResponse<T> { Data = data };
    }
}
