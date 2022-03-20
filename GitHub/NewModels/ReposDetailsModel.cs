namespace GitHub.NewModels
{
    public class ReposDetailsModel
    {
        public long Id { get; set; }
        public long ReposId { get; set;}
        public string ReposName { get; set;}
        public string? License { get; set; }
        public int StargazersCount { get; set; }
        public long GitHubOwnerId { get; set; }
        public string OwnerLogin { get; set; }
    }
}
