using Octokit;
using System.ComponentModel.DataAnnotations;

namespace GitHub.Models
{
    public class UserModel: Account
    {
        public User User{ get; set; }
        public IReadOnlyList<Repository> Repositories { get; set; }
    }
}
