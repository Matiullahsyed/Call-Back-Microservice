namespace CMCallbackMicroService.Domain.Interface
{
    public interface ICPSProcess
    {
        Task<dynamic> ProcessAsync(string request, string URL, string useCase);
    }
}
