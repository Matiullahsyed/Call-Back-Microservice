using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AccountManagementMicroService.Helpers
{
    public static class Utilities
    {

        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }
        public static DateTimeOffset CacheExpiry()
        {
            return DateTimeOffset.Now.AddMinutes(5);  
        }
        public static DateTimeOffset CacheBlockingExpiry()
        {
            return DateTimeOffset.Now.AddMinutes(600);
        }
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, SymmetricSecurityKey tokenKey)
        {
            var signingKey = tokenKey;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }

        public static string ObjectToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    serializer.Serialize(streamWriter, obj);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
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

        public static dynamic XmlToDynamic(string xmlString)
        {
            XElement root = XElement.Parse(xmlString);
            dynamic result = new ExpandoObject();

            // Add all the XML attributes to the dynamic object
            foreach (XAttribute attr in root.Attributes())
            {
                ((IDictionary<string, object>)result)[attr.Name.LocalName] = attr.Value;
            }

            // Add all the XML child elements to the dynamic object
            foreach (XElement elem in root.Elements())
            {
                if (elem.HasElements)
                {
                    ((IDictionary<string, object>)result)[elem.Name.LocalName] = XmlToDynamic(elem.ToString());
                }
                else
                {
                    ((IDictionary<string, object>)result)[elem.Name.LocalName] = elem.Value.Trim();
                }
            }

            return result;
        }
    }
}
