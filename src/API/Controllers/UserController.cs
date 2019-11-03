using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Filters;
using API.Models.ApiResponses;
using API.Models.User;
using API.Settings;
using Core.Services.Interfaces;
using Data.Request.User;
using Data.Response.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [ModelStateValidationFilter]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppSetting _appSetting;

        public UserController(IUserService userService, IOptions<AppSetting> appSetting)
        {
            _userService = userService;
            _appSetting = appSetting.Value ?? throw new ArgumentNullException(nameof(appSetting));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userService.GetAsync(model.Username, model.Password);
            return Ok(new ApiOkResponse(GenerateToken(user)));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            var user = await _userService.CreateAsync(request);
            return Ok(new ApiOkResponse(GenerateToken(user)));
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserRequest request)
        {
            var user = await _userService.UpdateAsync(id, request);
            return Ok(new ApiOkResponse(user));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Single(Guid id)
        {
            var user = await _userService.GetAsync(id);
            return Ok(new ApiOkResponse(user));
        }

        [HttpGet("ratings")]
        [Authorize]
        public async Task<IActionResult> Rating()
        {
            var authUserId =
                Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            return Ok(await _userService.GetRating(authUserId));
        }

        #region Helpers

        private string GenerateToken(UserResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username),
                    new Claim("firstname", user.Firstname),
                    new Claim("lastname", user.Lastname),
                    new Claim("phonenumber", user.PhoneNumber),
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}