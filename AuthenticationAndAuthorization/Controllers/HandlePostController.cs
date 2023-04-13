using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class HandlePostController : ControllerBase
    {
        private readonly BlogDataContext dataContext;

        public HandlePostController(BlogDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
        
            var posts = dataContext.Posts.ToList();
            return Ok(posts);
        }
    }
}
