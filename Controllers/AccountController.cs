using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dotnet_Api.Dtos;
using Dotnet_Api.Interfaces;
using Dotnet_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace Dotnet_Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AccountController(IUnitOfWork uow,IConfiguration config)
        {
            _config = config;
            _uow = uow;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await _uow.UserRepository.Authenticate(loginReq.Username,loginReq.Password);
            if (user== null)
            {
                return Unauthorized();
            }

            var loginRes= new LoginResDto();
            loginRes.Username=user.Username; loginRes.Token=CreateJwt(user);

            return Ok(loginRes);
        }

        [HttpPost("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]LoginReqDto data)
        {
            _uow.UserRepository.Register(data.Username,data.Password);
            var res=await _uow.SaveAsync();
            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users=await _uow.UserRepository.GetUsers();
            return Ok(users);
        }

        private string CreateJwt(User user)
        {
            var secretKey=_config.GetSection("Jwt:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims= new Claim[]{
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())               
            };

            var signingCredentials= new SigningCredentials(
                key,SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor= new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials=signingCredentials
            };

            var tokenHandler= new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}