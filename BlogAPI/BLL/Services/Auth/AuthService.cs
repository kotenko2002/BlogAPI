﻿using BlogAPI.DAL.Entities.Users;
using BlogAPI.PL.Common.Middlewares;
using BlogAPI.PL.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BlogAPI.BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _config;
        private readonly UserManager<User> _userManager;

        public AuthService(
            IOptions<JwtConfig> jwtOptions,
            UserManager<User> userManager)
        {
            _config = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            User existsUser = await _userManager.FindByNameAsync(request.Username);
            if (existsUser != null)
            {
                throw new BusinessException(HttpStatusCode.BadRequest, $"Запис з користувацьмис ім'ям {request.Username} вже існує!");
            }

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.Phone,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                string message = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BusinessException(HttpStatusCode.BadRequest, message);
            }
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            User user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new BusinessException(HttpStatusCode.BadRequest, "Неправильне користувацьке ім'я або пароль");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            JwtSecurityToken accessToken = GenerateAccessToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));

            return new JwtSecurityToken(
                issuer: _config.ValidIssuer,
                audience: _config.ValidAudience,
                expires: DateTime.Now.AddDays(_config.TokenValidityInDays),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
