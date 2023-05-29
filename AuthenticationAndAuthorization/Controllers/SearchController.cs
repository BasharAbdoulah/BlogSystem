using BlogSystem.DBModels;
using BlogSystem.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> Search(int skip, int take)
        {
            try
            {
                string searchTerm = Request.Query["searchTerm"].ToString();
                var posts = await unitOfWork.Post.All();
                var searchedPost = posts.Where(p => p.PostTitle.Contains(searchTerm));
                
                List<Post> paginatedPosts = searchedPost.ToList();


                if (skip != 0)
                {
                   paginatedPosts.RemoveRange(0, skip);
                }
                var takenPosts = paginatedPosts.Take(take);

                var response = new SearchResponse
                {
                    Posts = takenPosts,
                    TotalSearchedPosts = searchedPost.Count(),
                    totalPosts = posts.Count()
                };

                if (skip == 0 && take == 0)
                {
                    return Ok(posts);
                }
                return Ok(response);



            } catch (Exception ex) { return  BadRequest(ex.Message); }

        }

    public class SearchResponse
    {
        public IEnumerable<Post>  Posts { get; set;}
        public int TotalSearchedPosts { get; set; }
        public int totalPosts { get; set; }
    }

    }

}
