using Api.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.DataAccess
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Duration { get; set; }

        public Jwt(string? key,string? Duration)
        {
            this.Key = key ?? "";
            this.Duration = Duration??"";
        }

        public string GenerateTokens(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id",user.id.ToString()),
                new Claim("firstName",user.FirstName),
                new Claim("lastName",user.LastName),
                new Claim("mobile",user.Mobile),
                new Claim("email",user.Email),
                new Claim("blocked",user.Blocked.ToString()),
                new Claim("active",user.Active.ToString()),
                new Claim("createdAt",user.createdOn),
                new Claim("userType",user.UserType.ToString())
            };
            var JwtToken = new JwtSecurityToken
           (
               issuer: "localhost",
               audience: "loclhost",
               claims: claims,
               expires: DateTime.Now.AddMinutes(Int32.Parse(this.Duration)),
               signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(JwtToken);
        }
    }
}
