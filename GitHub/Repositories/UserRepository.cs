using GitHub.Data;
using GitHub.NewModels;
using GitHub.Repositories.Repository;
using Octokit;

namespace GitHub.Repositories
{
    public class UserRepository : Repository<UserModelNew>, IUserRepository
    {
        public UserRepository(AppDbContext context):base(context)
        {

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

            Add(obj);
        }
    }
}
