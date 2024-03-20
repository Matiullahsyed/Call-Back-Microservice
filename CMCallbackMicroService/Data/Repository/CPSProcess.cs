using AccountManagementMicroService.Helpers;
using CallbackMicroService.Domain.Interface;
using CMCallbackMicroService.Data.Context;
using CMCallbackMicroService.Domain.Interface;
using CMCallbackMicroService.Domain.Model.Request;
using CMCallbackMicroService.Domain.Model.Response;
using CMCallbackMicroService.Domain.Model.UseCasesResponse;
using static CMCallbackMicroService.Domain.Model.UseCasesResponse.LoginResponse;

namespace CMCallbackMicroService.Data.Repository
{
    public class CPSProcess : ICPSProcess
    {
        private readonly ICBService _cBService;
        private readonly IBaseRepository _baseRepository;
       
        public CPSProcess(ICBService cBService, IBaseRepository baseRepository) 
        {
            _cBService = cBService;
            _baseRepository = baseRepository;
        }

        public async Task<dynamic> ProcessAsync(string request, string URL, string useCase)
        {
            try
            {
                CPSResponse cPSResponse = await _baseRepository.SendAsync<CPSResponse>(request, URL);
                //var baseResponse = await _cBService.CBListener(request, URL);

               

                return cPSResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
