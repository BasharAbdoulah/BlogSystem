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


    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PostsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<Post>>> Get()
        {
            var posts = await unitOfWork.Post.All();
            return Ok(posts);
        }

        [HttpGet("postsByUserId/{id}")]
        public async Task<ActionResult<List<Post>>> GetByUserId(int id)
        {
            var posts = "";

            return Ok(posts);
        }

        [HttpPost, Authorize(Roles ="User")]
        public async Task<ActionResult<Post>> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            try
            {

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(post);
        }

        [HttpPut]
        public async Task<ActionResult<Post>> Edit(Post post)
        {
            var OldPost = "";
            if (OldPost == null) return BadRequest();

            try
            {
                //OldPost.PostContent = post.PostContent;
                //OldPost.PostTitle = post.PostTitle;
                //OldPost.Tags = post.Tags;
                //OldPost.PostImg = post.PostImg;
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return Ok("The post has been edited!");
        }

        [HttpDelete]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            var post = "";
            if (post == null) return BadRequest();
            try
            {
 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Post has been deleted");
        }
    }
}
