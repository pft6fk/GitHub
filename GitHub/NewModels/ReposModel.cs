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
        public string FullName { get; set; }
        public string? Language { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? License { get; set; }
        public int StargazersCount { get; set; }
        //public int N{ get; set; } 
        //public long OwnerId { get; set; }
        //public UserModelNew Owner { get; set; }
    }
}
