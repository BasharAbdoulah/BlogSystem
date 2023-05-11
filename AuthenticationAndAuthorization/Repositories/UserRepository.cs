using BlogSystem.Core;
using BlogSystem.DBModels;
using BlogSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlogDataContext context, ILogger logger) : base(context, logger)
        {
        }



    }
}
