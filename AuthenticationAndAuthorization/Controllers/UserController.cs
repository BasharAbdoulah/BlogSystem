using BlogSystem.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BlogDataContext context;

        public UserController(BlogDataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> get()
        {
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }
        //

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> get(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            else
                return Ok(user);
        }

        ////
        [HttpPost("Register")]
        public async Task<ActionResult<User>> PostBlog(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            else
                try
                {


                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            return CreatedAtAction(nameof(PostBlog), user);
        }

        //// 
        [HttpDelete, Authorize]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var blog = await context.Users.FindAsync(id);

            if (blog == null) { return BadRequest(); }
            else context.Users.Remove(blog);
            await context.SaveChangesAsync();
            return Ok("User has been deleted!");
        }

        ////
        [HttpPut, Authorize]
        public async Task<ActionResult<User>> Edit(User NewUser)
        {
            var user = await context.Users.FindAsync(NewUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            //user.FirstName = NewUser.FirstName;
            //user.LastName = NewUser.LastName;
            //user.Email = NewUser.Email;
            //user.Dob = NewUser.Dob;
            //user.Password = NewUser.Password;
            //user.ProfileImg = NewUser.ProfileImg;   


            await context.SaveChangesAsync();


            return Ok("The user has been edited");
        }
    }
}
