using GitHub.NewModels;
using Octokit;

namespace GitHub.Repositories.Repository
{
    public interface IContributorsRepository: IRepository<ContributorsModel>
    {
        public void AddToDb(RepositoryContributor contributors, long repoId, int numberOfContributors);
    }
}
