using GitHub.Data;
using GitHub.NewModels;
using GitHub.Repositories.Repository;
using Octokit;

namespace GitHub.Repositories
{
    public class ReposRepository: Repository<ReposModel>, IReposRepository
    {
        private readonly AppDbContext _context;
        public ReposRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public void AddToDb(Octokit.Repository repos)
        {
            var obj = new ReposModel();
            obj.GitHubId = repos.Id;
            obj.Name = repos.Name;
            obj.HtmlUrl = repos.HtmlUrl;
            obj.UpdatedAt = repos.UpdatedAt.LocalDateTime;
            //get owners detail ??????
            obj.GitHubOwnerId = repos.Owner.Id;
            obj.GitHubOwnerLogin = repos.Owner.Login;

            obj.StargazersCount = repos.StargazersCount;
            obj.Language = repos.Language;
            obj.FullName = repos.FullName;

            if (repos.License == null)
                obj.License = null;
            else
                obj.License = repos.License.Name;

            //obj.OwnerId = repos.Owner.Id;

            Add(obj);
        }

    }
}
