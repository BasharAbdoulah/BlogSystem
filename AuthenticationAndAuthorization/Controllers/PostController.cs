using BlogSystem.DBModels;
using BlogSystem.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BlogSystem.Controllers.SearchController;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public PostsController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get(int skip, int take)
        {
            var posts = await unitOfWork.Post.All();
            List<Post> result = posts.ToList();

            if (skip > 0) { 
                result.RemoveRange(0,skip);
            }
            var takenPosts = result.Take(take);

            if (skip == 0 && take == 0) {
                
                takenPosts = posts.ToList();

            }

            var response = new ResponseModel
            {
                Posts = takenPosts,
                Total = posts.Count()

            };
            return Ok(response);
        }

        [HttpGet("postsByUserId/{id}")]
        public async Task<ActionResult<List<Post>>> GetByUserId(int id)
        {

            try
            {
                var posts = await unitOfWork.Post.All();
                var postsByUser = posts.Where(x => x.UserId == id);
                
                return Ok(postsByUser);

            } catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [HttpGet("postsById/{id}")]
        public async Task<ActionResult<List<Post>>> GetById(int id)
        {
            var post = await GetByID(id);

            return Ok(post);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Post>> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            try
            {
                await unitOfWork.Post.Add(post);
                await unitOfWork.CompleteAsync();
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
            var OldPost = await GetByID(post.Id);
            if (OldPost == null) return BadRequest();

            try
            {
               await  unitOfWork.Post.Update(post);
                await unitOfWork.CompleteAsync();

            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return Ok("The post has been edited!");
        }

        [HttpDelete]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            var post = await GetByID(id);
            if (post == null) return BadRequest();
            try
            {
                await unitOfWork.Post.Delete(post);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Post has been deleted");
        }

        public Task<Post> GetByID(int id)
        {
            var post = unitOfWork.Post.GetById(id);
            return post;
        }
    }

    internal class ResponseModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public int Total { get; set; }
    }
}
