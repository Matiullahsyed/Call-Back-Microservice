using System.Xml.Serialization;

namespace CMCallbackMicroService.Domain.Model.UseCasesResponse
{
    [XmlRoot(ElementName = "Header", Namespace = "http://cps.huawei.com/cpsinterface/result")]
    public class Header
    {

        [XmlElement(ElementName = "Version", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public double Version { get; set; }

        [XmlElement(ElementName = "OriginatorConversationID", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public string OriginatorConversationID { get; set; }

        [XmlElement(ElementName = "ConversationID", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public string ConversationID { get; set; }
    }

    [XmlRoot(ElementName = "KYCValue", Namespace = "http://cps.huawei.com/cpsinterface/common")]
    public class KYCValue
    {

        [XmlAttribute(AttributeName = "nil", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public bool Nil { get; set; }
    }

    [XmlRoot(ElementName = "KYCField", Namespace = "http://cps.huawei.com/cpsinterface/common")]
    public class KYCField
    {

        [XmlElement(ElementName = "KYCName", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public string KYCName { get; set; }

        [XmlElement(ElementName = "KYCValue", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public string KYCValue { get; set; }
    }

    [XmlRoot(ElementName = "SimpleKYCData", Namespace = "http://cps.huawei.com/cpsinterface/result")]
    public class SimpleKYCData
    {

        [XmlElement(ElementName = "KYCField", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public List<KYCField> KYCField { get; set; }
    }

    [XmlRoot(ElementName = "IDRecord", Namespace = "http://cps.huawei.com/cpsinterface/common")]
    public class IDRecord
    {

        [XmlElement(ElementName = "IDTypeValue", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public int IDTypeValue { get; set; }

        [XmlElement(ElementName = "IDNumber", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public int IDNumber { get; set; }

        [XmlElement(ElementName = "DocumentReceived", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public string DocumentReceived { get; set; }
    }

    [XmlRoot(ElementName = "IDDetailsData", Namespace = "http://cps.huawei.com/cpsinterface/result")]
    public class IDDetailsData
    {

        [XmlElement(ElementName = "IDRecord", Namespace = "http://cps.huawei.com/cpsinterface/common")]
        public IDRecord IDRecord { get; set; }
    }

    [XmlRoot(ElementName = "QueryCustomerKYCResult", Namespace = "http://cps.huawei.com/cpsinterface/result")]
    public class QueryCustomerKYCResult
    {

        [XmlElement(ElementName = "BOCompletedTime", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public double BOCompletedTime { get; set; }

        [XmlElement(ElementName = "SimpleKYCData", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public SimpleKYCData SimpleKYCData { get; set; }

        [XmlElement(ElementName = "IDDetailsData", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public IDDetailsData IDDetailsData { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://cps.huawei.com/cpsinterface/result")]
    public class Body
    {

        [XmlElement(ElementName = "ResultType", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public int ResultType { get; set; }

        [XmlElement(ElementName = "ResultCode", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public int ResultCode { get; set; }

        [XmlElement(ElementName = "ResultDesc", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public string ResultDesc { get; set; }

        [XmlElement(ElementName = "QueryCustomerKYCResult", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public QueryCustomerKYCResult QueryCustomerKYCResult { get; set; }

        [XmlElement(ElementName = "Result", Namespace = "http://cps.huawei.com/cpsinterface/api_resultmgr")]
        public Result Result { get; set; }
    }

    [XmlRoot(ElementName = "Result", Namespace = "http://cps.huawei.com/cpsinterface/api_resultmgr")]
    public class Result
    {

        [XmlElement(ElementName = "Header", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "api", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Api { get; set; }

        [XmlAttribute(AttributeName = "com", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Com { get; set; }

        [XmlAttribute(AttributeName = "res", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Res { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class KYCResponse
    {

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }

        [XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soapenv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
