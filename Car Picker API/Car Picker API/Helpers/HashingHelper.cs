using System.Security.Cryptography;
using System.Text;

namespace Car_Picker_API.Helpers
{
    public static class HashingHelper
    {

        public static string HashValueWith384(string inputValue)
        {
            //convert string to bytes array
            var inputBytes = Encoding.UTF8.GetBytes(inputValue);
            //inlization hashing alogrthim object 
            var hasher = SHA384.Create();
            //compute hash
            var hashedByte = hasher.ComputeHash(inputBytes);
            //convert hashed byte to string 
            return BitConverter.ToString(hashedByte).Replace("-", "").ToLower();
        }
    }
}
