using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementMicroserviceTest.Helpers
{
    public class RequestSecurityTest
    {
        public static T Receive<T>(string encrypted_text)
        {
            using var aes = Aes.Create();

            aes.Key = Convert.FromBase64String("9qfGHcg0NlMac9pJr7phX6Jj+8dAm3vjjHb3rn839l4=");
            aes.IV = Convert.FromBase64String("vsuBiyBMi1xGEO3q1fRfZQ==");
            // Decrypt the message
            using (MemoryStream plaintext = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] encrypted_bytes = Convert.FromBase64String(encrypted_text);
                    cs.Write(encrypted_bytes, 0, encrypted_bytes.Length);
                    cs.Close();
                    string message = Encoding.UTF8.GetString(plaintext.ToArray());
                    var serilized_object = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
                    return serilized_object;
                }
            }
        }
    }
}
