using System.ComponentModel.DataAnnotations;

namespace GitHub.NewModels
{
    public class UserModelNew
    {
        [Key]
        public long Id { get; set; }
        public long GitHubUserId { get; set; }
        public string AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
    }
}
