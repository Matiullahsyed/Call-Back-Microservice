

using CMCallbackMicroService.Domain.Model.Response;
using CMCallbackMicroService.Domain.Model.UseCasesResponse;

namespace CallbackMicroService.Domain.Interface
{
    public interface ICBService
    {
        Task CBListener(string data);

    }
}
