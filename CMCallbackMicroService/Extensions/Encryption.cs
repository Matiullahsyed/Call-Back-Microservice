using java.security;
using System.Security.Cryptography;
using System.Text;

namespace AccountManagementMicroService.Extensions
{
    public class Encryption
    {
        private static byte[] privateKey { get; set; }
        public Encryption() {
            using (ECDiffieHellman alice = ECDiffieHellman.Create(ECCurve.NamedCurves.brainpoolP256r1))
            {
                var alicePublicKey = Convert.ToBase64String(alice.PublicKey.ToByteArray());

                var nodejsKey = "BFl6b6DW6E6vd4jdYpH0hwA9c+aJ4HstMPerBaZJ9h+kis7QkibOwt1p46QtDR4qLlgzFftraeHnFO+H8pu5Q6w="; //NODEJS brainpoolP256r1 publickey  base64
                byte[] nodejsKeyBytes = Convert.FromBase64String(nodejsKey);

                privateKey = getDeriveKey(nodejsKeyBytes, alice);
                byte[] encryptedMessage = null;
                byte[] iv = null;//new byte[] { 7, 242, 33, 110, 157, 125, 21, 234, 39, 234, 217, 212, 0, 77, 47, 115 };
                //Send(privateKey, "Secret message", out encryptedMessage, out iv);
                Receive(encryptedMessage, iv);
                var encryptedMsg = Convert.ToBase64String(encryptedMessage);
                Console.ReadLine();
            }
        }
        private void Send(byte[] key, string secretMessage, out byte[] encryptedMessage, out byte[] iv)
        {

            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                iv = aes.IV;

                // Encrypt the message
                using (MemoryStream ciphertext = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Encoding.UTF8.GetBytes(secretMessage);
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.Close();
                    encryptedMessage = ciphertext.ToArray();
                }
            }
        }
        private byte[] getDeriveKey(byte[] key1, ECDiffieHellman alice)
        {
            byte[] keyX = new byte[key1.Length / 2];
            byte[] keyY = new byte[keyX.Length];
            Buffer.BlockCopy(key1, 1, keyX, 0, keyX.Length);
            Buffer.BlockCopy(key1, 1 + keyX.Length, keyY, 0, keyY.Length);

            //Buffer.BlockCopy(key1, 0, keyX, 0, keyX.Length); 
            //Buffer.BlockCopy(key1, 0 + keyX.Length, keyY, 0, keyY.Length);
            ECParameters parameters = new ECParameters
            {
                Curve = ECCurve.NamedCurves.brainpoolP256r1,
                Q =
            {
                X = keyX,
                Y = keyY,
            },
            };
            byte[] derivedKey;
            using (ECDiffieHellman bob = ECDiffieHellman.Create(parameters))
            using (ECDiffieHellmanPublicKey bobPublic = bob.PublicKey)
            {
                return derivedKey = alice.DeriveKeyFromHash(bobPublic, HashAlgorithmName.SHA256);
            }
        }
        public void Receive(byte[] encryptedMessage, byte[] iv)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = privateKey;
                aes.IV = iv;
                // Decrypt the message
                using (MemoryStream plaintext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedMessage, 0, encryptedMessage.Length);
                        cs.Close();
                        string message = Encoding.UTF8.GetString(plaintext.ToArray());
                        Console.WriteLine(message);
                    }
                }
            }
        }

    }
}
