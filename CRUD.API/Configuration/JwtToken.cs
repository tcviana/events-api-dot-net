namespace CRUD.API.Configuration
{
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtToken
    {
        private readonly string secret;
        private readonly string issuer;
        private readonly string audience;

        public JwtToken(IConfiguration config) 
        {
            secret = config["JWT:key"];
            issuer = config.GetValue<string>("JWT:issuer");
            audience = config.GetValue<string>("JWT:audience");

        }

        public string GenerateJwtToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[] { new Claim(ClaimTypes.Name, userId)},
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}