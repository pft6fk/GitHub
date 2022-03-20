using GitHub.NewModels;

namespace GitHub.Models
{
    public class DetailsDb
    {
        public ReposModel Repository { get; set; }
        public IList<ContributorsModel> Contributors { get; set; }
    }
}
