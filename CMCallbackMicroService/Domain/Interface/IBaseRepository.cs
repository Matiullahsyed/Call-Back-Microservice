namespace CMCallbackMicroService.Domain.Interface
{
    public interface IBaseRepository
    {
        Task<T> SendAsync<T>(string request, string URL);
    }
}
