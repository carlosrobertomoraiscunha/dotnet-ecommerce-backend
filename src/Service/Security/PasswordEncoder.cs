using System;
using System.Text;

namespace Service.Security
{
    public class PasswordEncoder
    {
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encodeData = new byte[password.Length];
                encodeData = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encodeData);

                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encriptar senha " + ex.Message);
            }
        }
    }
}
