using System.Security.Cryptography;
using System.Text;

namespace AccountManagementMicroService.Extensions
{

    public static class RequestSecurity
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings."+ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json")
                    .Build();
        static byte[] Key
        {
            get
            {
                return Convert.FromBase64String(_configuration["Encryption_Decryption_Key"].ToString());
            }
        }
        static byte[] IV
        {
            get
            {
                return Convert.FromBase64String(_configuration["IV"].ToString());
            }
        }

        static void GetKeys()
        {
            //byte[] prKey = Convert.FromBase64String("Ca3wed53OmjlD3r3N2j1Wt6pK9rAFccxHZUtynv3ooU=");
            //byte[] pubKey = Convert.FromBase64String("BJ9MLHPToOYXfBwQd4/u+VpIfXYmX1SzHiPfgCuQlkuDOcE3JkZwF9o/DOmAul+xFbzUfbN1uftGDxdttkkXeTA=");

            ////Import public key
            //var ecdp = TlsEccUtilities.GetParametersForNamedCurve(NamedCurve.secp256r1);
            //var basePoint = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(null, ecdp, pubKey));
            //SubjectPublicKeyInfo subinfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(basePoint);
            //ECPublicKeyParameters publicKey = (ECPublicKeyParameters)PublicKeyFactory.CreateKey(subinfo);

            ////Import private key
            //BigInteger bi = new BigInteger(prKey);
            //ECPrivateKeyParameters privateKey = new ECPrivateKeyParameters(bi, publicKey.Parameters);

            //IBasicAgreement aKeyAgree = AgreementUtilities.GetBasicAgreement("ECDH");
            //aKeyAgree.Init(privateKey);
            //BigInteger sharedSecret = aKeyAgree.CalculateAgreement(publicKey);
            //byte[] sharedSecretBytes = sharedSecret.ToByteArray();

            //IDigest digest = new Sha256Digest();
            //byte[] symmetricKey = new byte[digest.GetDigestSize()];
            //digest.BlockUpdate(sharedSecretBytes, 0, sharedSecretBytes.Length);
            //digest.DoFinal(symmetricKey, 0);

        }
        public static String Send<T>(T data)
        {
            using var aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;

            // Encrypt the message
            using (MemoryStream ciphertext = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                String serilized_text = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                byte[] plaintextMessage = Encoding.UTF8.GetBytes(serilized_text);
                cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                cs.Close();
                return Convert.ToBase64String(ciphertext.ToArray());
            }

        }
        public static T Receive<T>(String encrypted_text)
        {
            using var aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;
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
        public static dynamic Receive(String encrypted_text)
        {
            using var aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;
            // Decrypt the message
            using (MemoryStream plaintext = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] encrypted_bytes = Convert.FromBase64String(encrypted_text);
                    cs.Write(encrypted_bytes, 0, encrypted_bytes.Length);
                    cs.Close();
                    string message = Encoding.UTF8.GetString(plaintext.ToArray());
                    var serilized_object = Newtonsoft.Json.JsonConvert.DeserializeObject(message);
                    return message;
                }
            }
        }
        public static String Send(String data)
        {
            using var aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;

            // Encrypt the message
            using (MemoryStream ciphertext = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                String serilized_text = data;
                Newtonsoft.Json.JsonConvert.SerializeObject(data);

                byte[] plaintextMessage = Encoding.UTF8.GetBytes(serilized_text);
                cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                cs.Close();
                return Convert.ToBase64String(ciphertext.ToArray());
            }

        }

    }
}
