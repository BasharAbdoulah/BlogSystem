using BlogSystem.Core;
using BlogSystem.DBModels;
using BlogSystem.Services;

namespace BlogSystem.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDataContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
