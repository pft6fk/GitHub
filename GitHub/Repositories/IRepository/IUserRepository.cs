using GitHub.NewModels;
using Octokit;

namespace GitHub.Repositories.Repository
{
    public interface IUserRepository : IRepository<UserModelNew>
    {
        public void AddToDb(User user);
    }
}
