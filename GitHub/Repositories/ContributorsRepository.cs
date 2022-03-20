using GitHub.Data;
using GitHub.NewModels;
using GitHub.Repositories.Repository;
using Octokit;

namespace GitHub.Repositories
{
    public class ContributorsRepository: Repository<ContributorsModel>, IContributorsRepository
    {
        private readonly AppDbContext _context;
        public ContributorsRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public void AddToDb(RepositoryContributor contributors, long repoId, int numberOfContributors)
        {
            var obj = new ContributorsModel();

            obj.Contributions = contributors.Contributions;
            obj.RepoId = repoId;
            obj.Login = contributors.Login;
            obj.NumberOfContributors = numberOfContributors;
            
            Add(obj);
        }

    }
}
