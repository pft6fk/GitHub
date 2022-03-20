using Octokit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHub.Models
{
    public class Details
    {
        public Repository Repository { get; set; }
        public IReadOnlyList<RepositoryContributor> Contributors { get; set; }

    }
}
