using System.Xml.Serialization;

namespace CMCallbackMicroService.Domain.Model.UseCasesResponse
{
    public class LoginResponse
    {
        // using System.Xml.Serialization;
        // XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
        // using (StringReader reader = new StringReader(xml))
        // {
        //    var test = (Envelope)serializer.Deserialize(reader);
        // }

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

        [XmlRoot(ElementName = "AccountBalanceItem", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public class AccountBalanceItem
        {

            [XmlElement(ElementName = "AccountHolderID", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public double AccountHolderID { get; set; }

            [XmlElement(ElementName = "AccountHolderPublicName", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string AccountHolderPublicName { get; set; }

            [XmlElement(ElementName = "AccountTypeName", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string AccountTypeName { get; set; }

            [XmlElement(ElementName = "AccountTypeAlias", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string AccountTypeAlias { get; set; }

            [XmlElement(ElementName = "AccountRuleProfileID", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public int AccountRuleProfileID { get; set; }

            [XmlElement(ElementName = "AccountRuleProfileName", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string AccountRuleProfileName { get; set; }

            [XmlElement(ElementName = "AccountNo", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public double AccountNo { get; set; }

            [XmlElement(ElementName = "AccountName", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string AccountName { get; set; }

            [XmlElement(ElementName = "AccountStatus", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string AccountStatus { get; set; }

            [XmlElement(ElementName = "Currency", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public string Currency { get; set; }

            [XmlElement(ElementName = "AvailableBalance", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public int AvailableBalance { get; set; }

            [XmlElement(ElementName = "ReservedBalance", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public int ReservedBalance { get; set; }

            [XmlElement(ElementName = "UnclearedBalance", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public int UnclearedBalance { get; set; }

            [XmlElement(ElementName = "CurrentBalance", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public int CurrentBalance { get; set; }
        }

        [XmlRoot(ElementName = "AccountBalanceData", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public class AccountBalanceData
        {

            [XmlElement(ElementName = "AccountBalanceItem", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public AccountBalanceItem AccountBalanceItem { get; set; }
        }

        [XmlRoot(ElementName = "QueryCustomerBalanceResult", Namespace = "http://cps.huawei.com/cpsinterface/result")]
        public class QueryCustomerBalanceResult
        {

            [XmlElement(ElementName = "BOCompletedTime", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public double BOCompletedTime { get; set; }

            [XmlElement(ElementName = "AccountBalanceData", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public AccountBalanceData AccountBalanceData { get; set; }
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

            [XmlElement(ElementName = "QueryCustomerBalanceResult", Namespace = "http://cps.huawei.com/cpsinterface/result")]
            public QueryCustomerBalanceResult QueryCustomerBalanceResult { get; set; }

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

            [XmlAttribute(AttributeName = "res", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Res { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class LoginResponses
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }

            [XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Soapenv { get; set; }

            [XmlText]
            public string Text { get; set; }
        }


    }
}
