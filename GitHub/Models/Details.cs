using Octokit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHub.Models
{
    public class Details : Repository
    {
        public Repository Repository { get; set; }
        public IReadOnlyList<RepositoryContributor> Contributors { get; set; }
        public Commit Commits { get; set; }
        public IReadOnlyList<GitHubCommit> Commit { get; set; }

    }
}
