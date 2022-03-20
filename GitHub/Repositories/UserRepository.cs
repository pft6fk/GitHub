using GitHub.Data;
using GitHub.NewModels;
using GitHub.Repositories.Repository;
using Octokit;

namespace GitHub.Repositories
{
    public class UserRepository : Repository<UserModelNew>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public void AddToDb(User user)
        {
            var obj = new UserModelNew();
            obj.GitHubUserId = user.Id;
            obj.Location = user.Location;
            obj.Following = user.Following;
            obj.Followers = user.Followers;
            obj.AvatarUrl = user.AvatarUrl;
            obj.Bio = user.Bio;
            obj.Company = user.Company;
            obj.Login = user.Login;
            obj.Name = user.Name;

            Add(obj);
        }

        public IEnumerable<ReposModel> GetAllUserRepos(int OwnerId)
        {
            var repos = from db in _context.Repos
                        where db.GitHubOwnerId == OwnerId
                        select db;
            return repos; 

        }
    }
}
