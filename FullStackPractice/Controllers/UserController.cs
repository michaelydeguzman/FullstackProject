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
    public class UserController : ControllerBase
    {
        private readonly ISecurityManager _securityManager;

        public UserController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndPoint()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var currentUser = _securityManager.GetCurrentUser(identity);

            return Ok($"Hi {currentUser.EmployeeName}, you are a/an {currentUser.Role}!");
        }
    }
}
