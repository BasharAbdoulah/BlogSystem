using BlogSystem.Services;

namespace BlogSystem.UOW
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IPostRepository Post { get; }
        ILikeRepository Like { get; }
        ICommentRepository Comment { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
