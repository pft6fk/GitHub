using GitHub.NewModels;

namespace GitHub.Repositories.Repository
{
    public interface IReposRepository: IRepository<ReposModel>
    {
        public void AddToDb(Octokit.Repository repos);
    }
}
