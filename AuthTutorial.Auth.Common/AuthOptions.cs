using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AuthTutorial.Auth.Common
{
    public class AuthOptions
    {
        //Автор токена
        public string Issuer { get; set; }
        //Для кого предназначен токен
        public string Audience { get; set; }
        //Строка для генерации 
        public string Secret { get; set; }
        //Длит жизни токена
        public int TokenLifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
