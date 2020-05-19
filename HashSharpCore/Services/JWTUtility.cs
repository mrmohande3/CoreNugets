using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HashSharpCore.DataLayer.Models;
using HashSharpCore.Models;
using HashSharpCore.Models.Contracts;
using HashSharpCore.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HashSharpCore.Services
{

    public class JWTUtility : IJwtUtility, IScopedDependency
    {
        private readonly SignInManager<User> _signInManager;
        private readonly SiteSettings _siteSettings;

        public JWTUtility(IOptionsSnapshot<SiteSettings> siteSettings, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _siteSettings = siteSettings.Value;
        }
        public async Task<AccessToken> Generate(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var signingCredintial = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512Signature);
            var claims = GetClaims(user);
            var desctiptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSettings.JwtSettings.Issuer,
                Audience = _siteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(_siteSettings.JwtSettings.ExpireAddDay),
                SigningCredentials = signingCredintial,
                Subject = new ClaimsIdentity(await GetClaims(user))
            };
            var handler = new JwtSecurityTokenHandler();
            return new AccessToken(handler.CreateJwtSecurityToken(desctiptor));
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            var claims = (await _signInManager.ClaimsFactory.CreateAsync(user)).Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Gender, user.Gender == 0 ? "Femail" : "Mail"));
            return claims;
        }
    }
}
