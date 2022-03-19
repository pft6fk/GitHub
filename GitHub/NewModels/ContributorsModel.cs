namespace GitHub.NewModels
{
    public class ContributorsModel
    {
        public long Id { get; set; }
        public long RepoId { get; set; }
        public ReposModel Repos { get; set; }
        public string Login { get; set; }
        public int Contributions { get; set; }
    }
}
