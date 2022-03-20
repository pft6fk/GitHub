using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHub.NewModels
{
    public class ReposModel
    {
        [Key]
        public long Id { get; set; }
        public long GitHubId { get; set; }
        public string Name { get; set; }
        public string HtmlUrl { get; set; }
        public string FullName { get; set; }
        public string? Language { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? License { get; set; }
        public int StargazersCount { get; set; }
        public long GitHubOwnerId { get; set; }
        public string GitHubOwnerLogin { get; set; }
    }
}
