using System.Xml;

namespace CallbackMicroService.SOAP_Helpers.SOAP_Models
{
    public class SoupResponse
    {
        public List<XmlNode> Header { get; set; }

        public List<XmlNode> Body { get; set; }
    }
}
