using CallbackMicroService.Logger;
using CMCallbackMicroService.Domain.Interface;
using CMCallbackMicroService.Domain.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMCallbackMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPSProcessController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly ICPSProcess _cpsProcess;

        public CPSProcessController(ILoggerService logger, ICPSProcess cpsProcess) 
        {
            _logger = logger;
            _cpsProcess = cpsProcess;
        }


        #region ProcessCall
        /// <summary>
        /// This Api/Method is written to Request Otp against given Packet. It accept user msidn and generate otp against them
        /// </summary>
        /// <param name="ProcessCall"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProcessCall")]
        public async Task<IActionResult> ProcessCall(KYCRequestModel request)
        {
            try
            {
                //Calling Repository function RequestOtpAsync to generate OTP against the given model
                var retData = await _cpsProcess.ProcessAsync(request.request, request.kycurl, request.usecase);

                return Ok(retData);
            }
            //This is exception case and for exception we return status code
            catch (Exception ex)
            {
                //Exception Logging
                _logger.LogError(ex, "RequestingOrganisationTransactionReference: ");

                return this.StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                new
                {
                    success = false,
                    responseCode = 500,
                    errordescription = "Internal Server Error"
                });
            }
        }
        #endregion
    }
}
