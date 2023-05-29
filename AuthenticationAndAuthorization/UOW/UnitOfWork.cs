using BlogSystem.Repositories;
using BlogSystem.Services;
using Microsoft.Extensions.Logging;

namespace BlogSystem.UOW
{
    public class  UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BlogDataContext _Context;
        private readonly ILogger logger;

        public IUserRepository User { get; private set; }
        public IPostRepository Post { get; private set; }

        public ILikeRepository Like { get; private set; }

        public ICommentRepository Comment { get; private set; } 

        public UnitOfWork(BlogDataContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            logger = loggerFactory.CreateLogger("logs");

            User = new UserRepository(context, logger);
            Post = new PostRepository(context, logger);
            Like = new LikeRepository(context, logger);
            Comment = new CommentRepository(context, logger);
        }

        public async Task CompleteAsync()
        {
            await _Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
