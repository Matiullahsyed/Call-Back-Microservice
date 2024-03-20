using AccountManagementMicroService.Cache;
using CallbackMicroService.Domain.Interface;
using CallbackMicroService.Logger;
using CMCallbackMicroService.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web.Http;

namespace AccountManagementMicroService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly ICacheService _cacheService;
        private readonly ICBService _cbService;

        public CallbackController(ILoggerService logger, ICacheService cacheService,ICBService cbservice)
        {
            _logger = logger;
            _cacheService = cacheService;
            _cbService = cbservice;
        }

        #region callbacklistener
        /// <summary>
        /// This API is used to get callback response
        /// </summary>
        /// <param name="callbacklistener"></param>
        /// <returns></returns>
        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //[Microsoft.AspNetCore.Mvc.Route("callbacklistener")]
        ////[ServiceFilter(typeof(EncryptionProviderFilter<CBRequest>))]
        ////[ServiceFilter(typeof(SessionValidationFilter))]
        //public async Task Callbacklistener()
        //{
        //    //This will get the data from encryption filter and map data into desire model
        //    // CBRequest cbrequest = (CBRequest)this.HttpContext.Items["modeldata"];
        //    try
        //    {
        //        //This will get the data from encryption filter and map data into desire model
        //        //BalanceDto balance_dto = (BalanceDto)this.HttpContext.Items["modeldata"];


        //        //Calling callback function to get callback response
        //       // var retData = _cbService.CBListener();
        //        //return retData;

        //        //Checking Return Response
        //        //if (retData.success == true)
        //        //{
        //        //    return this.StatusCode(StatusCodes.Status201Created, new
        //        //    {
        //        //        success = true,
        //        //        responseCode = retData.responseCode,
        //        //        transactionStatus = retData.responseMessage,
        //        //        responseData = new
        //        //        {
        //        //            main_balance = retData.responseData.main_balance,
        //        //            saving_balance = retData.responseData.saving_balance
        //        //        }
        //        //    });
        //        //}
        //        //else
        //        //{
        //        //    return this.StatusCode(StatusCodes.Status201Created, new
        //        //    {
        //        //        success = false,
        //        //        responseCode = retData.responseCode,
        //        //        errordescription = retData.responseMessage,
        //        //        responseData = new { }
        //        //    });
        //        //}
        //    }
        //    //This is exception case and for exception we return status code
        //    catch (Exception ex)
        //    {
        //        CBRequest cb = new CBRequest();
        //        //Exception Logging
        //        // _logger.LogError(ex, "Request: " + JsonSerializer.Serialize());
        //        // return cb;

        //        //return this.StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
        //        //new
        //        //{
        //        //    success = false,
        //        //    responseCode = 500,
        //        //    errordescription = "Internal Server Error"
        //        //});
        //    }
        //}
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("callbacklistener")]
        public Task callbacklistener([Microsoft.AspNetCore.Mvc.FromBody] string data)
        {
            _cbService.CBListener(data);

            // Process the callback data
            // Perform any necessary business logic or data processing

            // Send a response indicating successful processing, if required
            //return Microsoft.AspNetCore.Mvc.Ok();
            return null;
        }
        #endregion
    }
}
