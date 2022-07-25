using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Repository.Interfaces;
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
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userLogin)
        {
            var user = await Authenticate(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);

                return Ok(token);
            }

            return NotFound("User not found.");
        }

        private string GenerateToken(Employee user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.EmployeeName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<Employee> Authenticate(LoginUserDto userLogin)
        {
            var employees = (List<Employee>)await _unitOfWork.EmployeeRepository.GetAllAsync();

            var currentUser = employees.FirstOrDefault(x => x.Email.ToLower() == userLogin.Email.ToLower() && x.Password == userLogin.Password);

            if (currentUser == null)
            {
                return currentUser;
            }

            return currentUser;
        }
    }
}
