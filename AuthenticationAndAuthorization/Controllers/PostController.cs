using BlogSystem.DBModels;
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
        private readonly BlogDataContext dBContext;

        public PostsController(BlogDataContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<Post>>> Get()
        {
            var posts = await dBContext.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet("postsByUserId/{id}")]
        public async Task<ActionResult<List<Post>>> GetByUserId(int id)
        {
            var posts = await dBContext.Posts.Where(p => p.UserId == id).ToListAsync();

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
                dBContext.Posts.Add(post);
                await dBContext.SaveChangesAsync();
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
            var OldPost = await dBContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (OldPost == null) return BadRequest();

            try
            {
                OldPost.PostContent = post.PostContent;
                OldPost.PostTitle = post.PostTitle;
                OldPost.Tags = post.Tags;
                OldPost.PostImg = post.PostImg;
                await dBContext.SaveChangesAsync();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return Ok("The post has been edited!");
        }

        [HttpDelete]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            var post = await dBContext.Posts.FirstOrDefaultAsync(post => post.Id == id);
            if (post == null) return BadRequest();
            try
            {
                dBContext.Posts.Remove(post);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Post has been deleted");
        }
    }
}
