using System.Security.Cryptography;
using System.Text;

namespace Repository.Helpers
{
    public static class PasswordHelper
    {
        public static byte[] CryptoPassword(string password)
        {
            if (password == null)
                return null;
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            return md5.Hash;
        }

        public static bool VerifyPassword(string password, byte[] userPass)
        {
            byte[] crypted = CryptoPassword(password);
            var verify = !crypted.Where((t, i) => t != userPass[i]).Any();
            return verify;
        }
    }
}
