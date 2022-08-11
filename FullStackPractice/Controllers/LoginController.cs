using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISecurityManager _securityManager;

        public LoginController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userLogin)
        {
            var user = await _securityManager.Authenticate(userLogin);

            if (user != null)
            {
                var token = _securityManager.GenerateToken(user);

                LoginDetailDto loginDetails = new LoginDetailDto
                {
                    Email = user.Email,
                    EmployeeId = user.EmployeeId,
                    EmployeeName = user.EmployeeName,
                    Role = user.Role,
                    Token = token
                };

                return Ok(loginDetails);
            }

            return NotFound("User not found.");
        }

       
    }
}
