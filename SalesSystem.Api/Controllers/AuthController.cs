using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.Enums;
using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;
using SalesSystem.Service.Services.Abstract;
using SalesSystem.Service.Services.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace SalesSystem.Api.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IApplicationUserService _userService;
        private readonly IConfiguration _configuration;


        public AuthController(IApplicationUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userService.FindByEmailAsync(model.Email);
            if (user != null && await _userService.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,user.UserType.ToString()),
                };

                var token = TokenService.CreateToken(authClaims, _configuration);
                var refreshToken = TokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userService.UpdateTokenAsync(user);

                return Ok(new
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenViewModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = TokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }
            string username = principal.Identity.Name;

            var user = await _userService.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime >= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = TokenService.CreateToken(principal.Claims.ToList(), _configuration);
            var newRefreshToken = TokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userService.UpdateTokenAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            var res = await _userService.CreateAsync(model, model.Password!);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User model)
        {
            var res = await _userService.UpdateAsync(model);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var res = await _userService.DeleteAsync(new User() { Id = id });
            return Ok(res);
        }

        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var res = await _userService.FindByNameAsync(name);
            return Ok(res);
        }

        [HttpGet("GetByEmailAsync")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var res = await _userService.FindByEmailAsync(email);
            return Ok(res);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            var res = await _userService.GetByIdAsync(id);
            return Ok(res);
        }

        
    }
}
