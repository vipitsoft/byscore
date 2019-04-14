using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Http;

namespace BYSCORE.UI.Common.Outer
{
    public class TokenProvider
    {
        private readonly TokenProviderOptions _options;
        private readonly UserService userService;
        public TokenProvider(TokenProviderOptions options, UserService _userService)
        {
            _options = options;
            userService = _userService;
        }

        public async Task<TokenEntity> GenerateToken(HttpContext httpContext, string userName, string password)
        {
            var identity = await GetIdentity(userName);
            if (identity == null)
            {
                return null;
            }

            password = Encryption.MD5Str(password);
            var user = await userService.UserLogin(new User { UserName = userName, PassWord = password });
            if (user == null)
            {
                return new TokenEntity
                {
                    Status = 0,
                    Message = "登录失败，用户名错误！"
                };
            }

            if (user.IsFreeze)
            {
                return new TokenEntity
                {
                    Status = 0,
                    Message = "登录失败，用户已冻结，请联系管理员解冻！"
                };
            }

            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
            new Claim (JwtRegisteredClaimNames.Sub,userName),
            new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim (JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
            new Claim (ClaimTypes.Name, user.UserName, ClaimValueTypes.String),
            new Claim (ClaimTypes.Sid, user.Code, ClaimValueTypes.String),
            new Claim(ClaimTypes.UserData, user.ToJSON(), ClaimValueTypes.String)
            };

            // Jwt安全令牌
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials
            );
            // 生成令牌字符串
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new TokenEntity
            {
                Status = 1,
                UserCode = user.Code,
                Message = "登录成功！",
                AccessToken = encodedJwt,
                Expires = (int)_options.Expiration.TotalSeconds
            };
            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// 查看登录令牌
        /// </summary>
        /// <returns>The identity.</returns>
        /// <param name="username">Username.</param>
        private Task<ClaimsIdentity> GetIdentity(string username)
        {
            return Task.FromResult(
                new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "token"),
                new Claim[] {
                    new Claim(ClaimTypes.Name, username)
                }
                ));
        }
    }
}
