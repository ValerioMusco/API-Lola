using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_Lola.Tools {
    public class TokenManager {

        public static string _secretKey = @"odInJUGtnjRrj44wSFg1HU1hpSfv1csbCtZo69mVa/L+NHPaAMUTgFIOOuQvbfxi";

        public string GenerateToken() {

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_secretKey));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

            // 2 : Données du token et de l'user
            //Claim[] claims = new[] {

            //    new Claim(ClaimTypes.Sid, user.Id.ToString()),
            //    new Claim(ClaimTypes.Role, user.RoleName)
            //};

            // 3 : Construction du token
            JwtSecurityToken jwt = new(
                //claims : claims,
                signingCredentials : signingCredentials,
                expires : DateTime.Now.AddDays(3650)
                // Gérer faire les urls
                //issuer : "monserverapi.com",
                //audience : "monsite.com"
            );

            JwtSecurityTokenHandler jwtHandler = new();
            return jwtHandler.WriteToken( jwt );
        }
    }
}
