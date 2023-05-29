using BlogSystem.Core;
using BlogSystem.DBModels;
using BlogSystem.Services;

namespace BlogSystem.Repositories
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(BlogDataContext context, ILogger logger) : base(context, logger) 
        {
            
        }
    }
}
