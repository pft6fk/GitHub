using GitHub.NewModels;

namespace GitHub.Models
{
    public class UserDetailsModel
    {
        public UserModelNew User{ get; set;}
        public IEnumerable<ReposModel> Repos { get; set;}
    }
}
