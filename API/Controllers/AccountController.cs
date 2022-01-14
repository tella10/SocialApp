using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPiController
    {
        private readonly DataContext _context;
        private readonly ITokenServices _tokenservice;

        public AccountController(DataContext context, ITokenServices tokenservice)
        {
            _tokenservice = tokenservice;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDtos>> Register(RegisterDto registerDto)
        {
            if (await isUsernameExist(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
            var user = new AppUsers
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDtos{
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtos>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                 .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512();
            var computePassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i > computePassword.Length; i++)
            {
                if (computePassword[i] != user.PasswordHash[i]) return Unauthorized("Incorrect Password");
            }

            return new UserDtos{
                Username = user.UserName.ToLower(),
                Token = _tokenservice.CreateToken(user)
            };
        }

        private async Task<bool> isUsernameExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}