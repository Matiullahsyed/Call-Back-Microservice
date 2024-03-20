using AccountManagementMicroService.Domain.Model.Response;
using AccountManagementMicroService.Helpers;
using CallbackMicroService.Domain.Interface;
using CallbackMicroService.Logger;
using CallbackMicroService.SOAP_Helpers.SOAP_Models;
using CMCallbackMicroService.Data.Context;
using CMCallbackMicroService.Data.Entities;
using CMCallbackMicroService.Domain.Interface;
using CMCallbackMicroService.Domain.Model;
using CMCallbackMicroService.Domain.Model.Response;
using CMCallbackMicroService.Domain.Model.UseCasesResponse;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace CallbackMicroService.Data.Repository
{
    public class CBService : ICBService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;
        private readonly CMCallbackEFContext _callbackEFContext;
        private readonly IBaseRepository _baseRepository;
        public CBService(IConfiguration configuration, ILoggerService logger,CMCallbackEFContext callbackEFcontext, IBaseRepository baseRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _callbackEFContext = callbackEFcontext;
            _baseRepository = baseRepository;
        }
        public async Task CBListener(string callbackData)
        {
            CPSResponse cPSResponse = new();
            try
            {
  
                //SoupResponse response = new SoupResponse();

                //payload.ResultURL = _configuration.GetValue<string>("CallbackURL");

                
                //listener.Prefixes.Add(payload.ResultURL);
                //listener.Start();
                //CPSResponse cPSResponse = await _baseRepository.SendAsync<CPSResponse>(request, URL);
                //HttpListenerContext context = listener.GetContext();

                //HttpListenerRequest requests = context.Request;
                //string documentContents;
                //using (Stream receiveStream = requests.InputStream)
                //{
                //    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                //    {
                //        documentContents = readStream.ReadToEnd();
                //    }
                //}
                try
                {
                    var client = new HttpClient();
                    callbackrecord cEF = new callbackrecord() { };
                    if (callbackData != "")
                    {
                        cEF.conversationid = cPSResponse.Body.Response.Header.ConversationID != null ? cPSResponse.Body.Response.Header.ConversationID : "";
                        cEF.cpsresponse = callbackData;
                        var apiRequest = JsonSerializer.Serialize(cEF);
                        var content = new StringContent(apiRequest, null, "application/json");
                        await _callbackEFContext.callbackrecord.AddAsync(cEF);
                        await _callbackEFContext.SaveChangesAsync();
                        _logger.LogInformation("CallBack Response Payload: " + JsonSerializer.Serialize(cEF));
                        if (cPSResponse.Body.Response.Body.ResponseCode == 0)
                        {
                            var data = _callbackEFContext.callbackrecord.FirstOrDefault(x => x.conversationid == cPSResponse.Body.Response.Header.ConversationID);

                            //if (data != null)
                            //{
                            //    return data.cpsresponse;
                            //}
                        }

                    }

                   // return null;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CallBack: " + callbackData);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CallBack failed: " + callbackData);
                throw;
            }
        }
        async void Process(object o)
        {
            var context = o as HttpListenerContext;
            HttpListenerRequest request = context.Request;
            string documentContents;
            using (Stream receiveStream = request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(documentContents);
            // Set up the namespace manager
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("res", "http://cps.huawei.com/cpsinterface/result");


            // Retrieve the value of the SOAP tag by namespace
            XmlNode headerTag = xmlDoc.SelectSingleNode("//res:Header", namespaceManager);
            XmlNode bodyTag = xmlDoc.SelectSingleNode("//res:Body", namespaceManager);
            SoupResponse response = new SoupResponse();

            if (headerTag.HasChildNodes)
            {
                response.Header = new List<XmlNode>();
                foreach (XmlNode tag in headerTag.ChildNodes)
                {
                    response.Header.Add(tag);
                }
            }
            if (bodyTag.HasChildNodes)
            {
                response.Body = new List<XmlNode>();
                foreach (XmlNode tag in bodyTag.ChildNodes)
                {
                    response.Body.Add(tag);
                }
            }
            try
            {
                var client = new HttpClient();
                callbackrecord cEF = new callbackrecord();
                if (response != null)
                {
                    cEF.conversationid = response.Header.FirstOrDefault(t => t.LocalName == "ConversationID").InnerText != null ? response.Header.FirstOrDefault(t => t.LocalName == "ConversationID").InnerText : "";
                    cEF.cpsresponse = documentContents;
                    var apiRequest = JsonSerializer.Serialize(cEF);
                    var content = new StringContent(apiRequest, null, "application/json");
                        _callbackEFContext.callbackrecord.Add(cEF);
                        _callbackEFContext.SaveChanges();
                    _logger.LogInformation("CallBack Response Payload: " + JsonSerializer.Serialize(cEF));

                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CallBack: " + response);
            }
        }
        public static dynamic XmlToObject<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
