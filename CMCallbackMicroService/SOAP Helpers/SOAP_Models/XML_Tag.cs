namespace AccountManagementMicroService.SOAP_Helpers.SOAP_Models
{
    public class XML_Tag
    {
        public String tag_name { get; set; }
        public String tag_value { get; set; }
        public List<XML_Tag> childs { get; set; }
    }
}
