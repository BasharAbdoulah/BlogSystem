using BlogSystem.Services;

namespace BlogSystem.UOW
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IPostRepository Post { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
