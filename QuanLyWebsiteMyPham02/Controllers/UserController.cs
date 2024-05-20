using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLMP.Common.Rsp;
using QLMP.Common.Req;
using QLMP.DAL;
using QLMP.DAL.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using QLMP.BLL;

namespace QLMP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QuanLyMyPhamContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly UserSvc _userSvc;

        public UserController(QuanLyMyPhamContext context, IOptionsMonitor<JwtSettings> optionsMonitor, UserSvc userSvc)
        {
            _context = context;
            _jwtSettings = optionsMonitor.CurrentValue;
            _userSvc = userSvc;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginReq model)
        {
            var res = _userSvc.AuthenticateUser(model);
            if (!res.Success)
            {
                res.SetMessage("Invalid username/password");
                return Ok(res);
            }

            var user = (User1)res.Data;
            var token = GenerateToken(user);
            var response = new SingleRsp();
            response.Data=token;
            response.SetMessage("Authenticate success");
            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register(UserReq model)
        {
            var res = _userSvc.CreateUser(model);
            if (!res.Success)
            {
                return Ok(res);
            }
            res.SetMessage("User registered successfully");
            res.Data = model;
            return Ok(res);
        }

        private string GenerateToken(User1 user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.securitykey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    
                    new Claim("UserName", user.UserName),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Role", user.Role),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
