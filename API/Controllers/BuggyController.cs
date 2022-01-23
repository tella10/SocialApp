using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseAPiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }


     
        [HttpGet("Auth")]
        public ActionResult<string> GetSecret(){

            return Unauthorized("Unauthorized");
            
        }


       
        [HttpGet("Not-found")]
        public ActionResult<AppUsers> GetNotFound(){

            var thing = _context.Users.Find(-1);

            if (thing == null) return  NotFound();

            return Ok(thing);
            
        }

      
        [HttpGet("Server-error")]
        public ActionResult<string> GetServerError(){

            var thing = _context.Users.Find(-1);

            var thing_to_return = thing.ToString();

            return thing_to_return;
            
        }


       
        [HttpGet("Bad-request")]
        public ActionResult<string> GetBadRequest(){

            return BadRequest("Bad Request");
            
        }

    }
}