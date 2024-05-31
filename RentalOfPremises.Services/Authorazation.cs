using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RentalOfPremises.Services
{
    public class Authorazation
    {
        public const string ISSUER = "https://localhost:5555/";
        public const string AUDIENCE = "https://localhost:5555/";
        const string KEY = "mysuperSecretKey@123456";
        public const int LIFETIME = 500;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
