using System;
using System.Security.Cryptography;
using System.Text;

namespace AppBusiness.Data.Hashs
{
    public static class SecurityHash
    {
        private const string SecurityKey = "BSA@KeyAndRomeDa!412^12Bs";

        public static string EncryptPlainTextToCipherText(string plainText)
        {
            var toEncryptedArray = Encoding.UTF8.GetBytes(plainText);

            var objMd5CryptoService = new MD5CryptoServiceProvider();
            var securityKeyArray = objMd5CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));
            objMd5CryptoService.Clear();

            var objTripleDesCryptoService = new TripleDESCryptoServiceProvider
            {
                Key = securityKeyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var objCrytpoTransform = objTripleDesCryptoService.CreateEncryptor();
            var resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDesCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptCipherTextToPlainText(string cipherText)
        {
            try
            {
                var toEncryptArray = Convert.FromBase64String(cipherText);
                var objMd5CryptoService = new MD5CryptoServiceProvider();

                var securityKeyArray = objMd5CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));
                objMd5CryptoService.Clear();

                var objTripleDesCryptoService = new TripleDESCryptoServiceProvider
                {
                    Key = securityKeyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var objCrytpoTransform = objTripleDesCryptoService.CreateDecryptor();
                var resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                objTripleDesCryptoService.Clear();

                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return cipherText;
            }
        }
    }
}
