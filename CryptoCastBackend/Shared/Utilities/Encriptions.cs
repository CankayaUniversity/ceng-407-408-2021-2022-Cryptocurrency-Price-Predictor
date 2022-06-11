using System.Security.Cryptography;
using System.Text;

namespace Shared.Utilities
{
    public static class Encriptions
    {
        private static readonly string _key= "7c8e135941d0681491b64a8318abfa8b753e57322cc4751f464376311f7f8bbc";
        
        #region Encrypt
        public static string EncryptText(string password)
        {

            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(password);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(_key);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 8, 2, 8, 3, 8, 5, 8, 0 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }


        #endregion
        
        #region Decrypt

        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 8, 2, 8, 3, 8, 5, 8, 0 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Padding= PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        public static string DecryptText(string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(password);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(_key);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        #endregion

        #region HexToStr

        public static string HexToStr(string hex)
        {
            List<string> strsplit = new List<string>();
            for (int i = 0; i < hex.Length; i = i + 2)
            {
                strsplit.Add(hex.Substring(i, 2));
            }
            StringBuilder sb = new StringBuilder();
            foreach (String hexa in strsplit)
            {
                int values = Convert.ToInt32(hexa, 16);
                string strvalue = Char.ConvertFromUtf32(values);
                sb.Append(strvalue);
            }
            return sb.ToString();
        }

        #endregion

    }
}
