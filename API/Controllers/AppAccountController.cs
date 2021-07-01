using HM.Common.Constant;
using HM.Model.RequestModel;
using HM.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppAccountController : ControllerBase
    {
        private IAppAccountService _appAccountService;
        public AppAccountController(IAppAccountService appAccountService)
        {
            _appAccountService = appAccountService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequestModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
                var user = this._appAccountService.Login(login.Username, login.Password);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppConst.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("AccountId",user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                //user.Roles = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(roles)));
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest("Tên đăng nhập và mật khẩu không đúng. Vui lòng thử lại");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Registration")]
        public IActionResult RegistrationAccount(AppAccountRegisterRequestModel newItem)
        {
            try
            {
                var baseUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
                var account = this._appAccountService.RegisterAccount(newItem, Directory.GetCurrentDirectory(), baseUrl);
                if (account == null)
                {
                    return BadRequest("Có lỗi xảy ra vui lòng thử lại");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UploadAvata")]
        public IActionResult UploadAvata(IFormFile newItem)
        {
            try
            {
                var phoneNumber = User.Identity.Name;
                var baseUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
                this._appAccountService.UploadAvatar(newItem, Directory.GetCurrentDirectory(), baseUrl, phoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(string oldPass, string newPass)
        {
            try
            {
                var phoneNumber = User.Identity.Name;
                this._appAccountService.ChangePassword(oldPass, newPass, phoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
