using BlogSystem.Core;
using BlogSystem.DBModels;
using BlogSystem.UOW;
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
        private readonly IUnitOfWork unitOfWork;


        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> get()
        {
    
               try
            {
                var users = await unitOfWork.User.All();
                return Ok(users);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        //

        [HttpGet("GetById/{id}"), Authorize]
        public async Task<ActionResult<User>> getUserById(int id)
        {
            var user = await GetById(id);
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
                    unitOfWork.User.Add(user);
                    unitOfWork.CompleteAsync();
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
            var blog = await GetById(id);

            if (blog == null) { return BadRequest(); }
            else await unitOfWork.User.Delete(blog);
           await unitOfWork.CompleteAsync();
            return Ok("User has been deleted!");
        }

        ////
        [HttpPut, Authorize]
        public async Task<ActionResult<User>> Edit(User NewUser)
        {
            try
            {
                var user = await GetById(NewUser.Id);
                await unitOfWork.User.Update(user);

                await unitOfWork.CompleteAsync();
                if (user == null)
                {
                    return NotFound();
                }
            } catch (Exception ex) { return BadRequest(ex.Message); }; 

 
            return Ok("The user has been edited");
        }

        public Task<User> GetById(int id)
        {
            var user = unitOfWork.User.GetById(id);
            return user;
        }
    }
}
