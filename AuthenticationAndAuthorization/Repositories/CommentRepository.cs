using BlogSystem.Core;
using BlogSystem.DBModels;
using BlogSystem.Services;

namespace BlogSystem.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDataContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
