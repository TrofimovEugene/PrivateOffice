using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrivateOfficeWebApp
{
	public class AuthOptions
	{
		public const string ISSUER = "PrivateOfficeServer"; // издатель токена
		public const string AUDIENCE = "PrivateOfficeClient"; // потребитель токена
		const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
		public const int LIFETIME = 100000; // время жизни токена - 1 минута
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
    }
}
