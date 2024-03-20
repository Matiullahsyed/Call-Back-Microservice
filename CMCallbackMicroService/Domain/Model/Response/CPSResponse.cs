using System.Xml.Serialization;

namespace CMCallbackMicroService.Domain.Model.Response
{
    [XmlRoot(ElementName = "Header", Namespace = "http://cps.huawei.com/cpsinterface/response")]
    public class Header
    {

        [XmlElement(ElementName = "Version", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public double Version { get; set; }

        [XmlElement(ElementName = "OriginatorConversationID", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public string OriginatorConversationID { get; set; }

        [XmlElement(ElementName = "ConversationID", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public string ConversationID { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://cps.huawei.com/cpsinterface/response")]
    public class Body
    {

        [XmlElement(ElementName = "ResponseCode", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public int ResponseCode { get; set; }

        [XmlElement(ElementName = "ResponseDesc", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public string ResponseDesc { get; set; }

        [XmlElement(ElementName = "ServiceStatus", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public int ServiceStatus { get; set; }

        [XmlElement(ElementName = "Response", Namespace = "http://cps.huawei.com/cpsinterface/api_requestmgr")]
        public Response Response { get; set; }
    }

    [XmlRoot(ElementName = "Response", Namespace = "http://cps.huawei.com/cpsinterface/api_requestmgr")]
    public class Response
    {

        [XmlElement(ElementName = "Header", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://cps.huawei.com/cpsinterface/response")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "api", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Api { get; set; }

        [XmlAttribute(AttributeName = "res", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Res { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CPSResponse
    {

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soapenv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
