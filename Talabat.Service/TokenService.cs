using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateToken(AppUser user,UserManager<AppUser> userManager)
        {
            //PayLoad       
            var AuthClaim = new List<Claim>()
             {
                 new Claim(ClaimTypes.GivenName,user.UserName),
                 new Claim(ClaimTypes.Email,user.Email),
             };
            var UserRoles = await userManager.GetRolesAsync(user);
            foreach (var Role in UserRoles)
                AuthClaim.Add(new Claim(ClaimTypes.Role, Role));
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims:AuthClaim,
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
