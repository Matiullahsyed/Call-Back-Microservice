using com.sun.xml.@internal.bind.v2.runtime.unmarshaller;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AccountManagementMicroService.SOAP_Helpers.SOAP_Models
{
    public static class XmlHelper
    {
        //public static T FromXml<T>(this string value)

        //{
        //    //value = "<? xml version='1.0' encoding='UTF-8' ?>" + value;
        //    using TextReader reader = new StringReader(value);
        //    return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        //}
        public static XML_Tag FromKeyPair(XElement element) {
             
            String name = String.Empty, value = String.Empty;
            element.Elements().ToList().ForEach(e => {
                if (e.Name.LocalName.Equals("key"))
                {
                    name = e.Value.ToString();
                }
                if (e.Name.LocalName.Equals("value"))
                {
                    value = e.Value.ToString();
                }
            });
            return new XML_Tag { tag_name = name, tag_value = value };
        }
        //public static List<XML_Tag> RetrieveData(IEnumerable<XElement> elements) {
        //    List<XML_Tag> data= new List<XML_Tag>();
        //    elements.ToList().ForEach(e => {
                
        //        if (!e.HasElements)
        //            data.Add(new XML_Tag { tag_name = e.Name.LocalName, tag_value = e.Value });
        //        else {
        //            List<XML_Tag> balances = new List<XML_Tag>();

        //            e.Elements().ToList().ForEach(b => {
        //                if (b.HasElements && b.Name.LocalName.Equals("keyValuePair"))
        //                {
        //                    balances.Add((XML_Tag)XmlHelper.FromKeyPair(b));
        //                }
        //            });
        //            data.Add(new XML_Tag { childs = balances });
        //        }
        //    });
        //    return data;
        //}
    }
}
