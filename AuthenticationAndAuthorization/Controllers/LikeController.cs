using BlogSystem.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class LikeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly BlogDataContext dataContext;

        public LikeController(IConfiguration configuration, BlogDataContext dataContext)
        {
            this.configuration = configuration;
            this.dataContext = dataContext;
        }

        [HttpPost]
        public async Task<ActionResult<Like>> Post(Like like)
        {
            if (like == null) return BadRequest();

            try
            {
                dataContext.likes.Add(like);
                await dataContext.SaveChangesAsync();

            } catch (Exception ex) 
            { BadRequest(ex.Message); }

            return Ok();
        }

        //Get Likes by post id
        [HttpGet("LikesByPost")]
        public async Task<ActionResult<List<Like>>> Get(int id)
        {
            var likesByPost = await dataContext.likes.Where(l => l.PostId == id).ToListAsync();
            return Ok(likesByPost);
        }

        [HttpDelete]
        public async Task<ActionResult<Like>> Delete(int id)
        {
            var like = dataContext.likes.FirstOrDefault(l => l.ID  == id);
            if (like == null) return BadRequest();

            dataContext.likes.Remove(like);
            await dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
