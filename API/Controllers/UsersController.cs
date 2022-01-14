using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseAPiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<AppUsers>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        //api/user/1
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<AppUsers>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}