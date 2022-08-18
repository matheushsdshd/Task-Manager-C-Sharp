using dotenv.net;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Manager_C_Sharp.Models;

namespace Task_Manager_C_Sharp.Services
{
    public class TokenService
    {
        public static string CreateToken(User user)
        {
            var envVars = DotEnv.Read();
            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptionKeyBytes = Encoding.ASCII.GetBytes(envVars["SECRET_KEY"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encryptionKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
