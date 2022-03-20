using GitHub.Data;
using GitHub.Models;
using GitHub.NewModels;
using GitHub.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Diagnostics;

namespace GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GitHubClient client;
        private readonly Credentials tokenAuth;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, AppDbContext context)
        {
            _logger = logger;

            client = new GitHubClient(new ProductHeaderValue("test"));

            tokenAuth = new Credentials("%your_credentialas%"); //here you shoyld put your credentials in order to prevent limits of queries
            client.Credentials = tokenAuth;
            _context = context;

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetRepo(string search)
        {   
            //https://api.github.com/search/repositories?q=tetris
            //pulls repositories with the name of tetris
            IEnumerable<ReposModel> repos = null;
            if (IfRepoExists(search) != null)
            {
                repos = IfRepoExists(search);

                return View("GetRepoDb", repos);
            }
            else
            {
                var request = new SearchRepositoriesRequest(search);
                var result = await client.Search.SearchRepo(request);

                foreach (var item in result.Items)
                {
                    _unitOfWork.ReposRepository.AddToDb(item);
                    _unitOfWork.Save();
                }
                return View(result.Items);
            }
        }
        public IList<ReposModel>? IfRepoExists(string search)
        {
            IEnumerable<ReposModel> RepoDb = _unitOfWork.ReposRepository.GetAll();
            //IEnumerable<ReposModel> FoundRepos = 
            //    from repo in RepoDb
            //    where repo.Name.Contains(search)
            //    select repo;

            List<ReposModel> reposModels = new List<ReposModel>();
            foreach (var item in RepoDb)
            {
                if (item.Name.Contains(search))
                {
                    reposModels.Add(item);
                }
            }
            if(reposModels.Any())
                return reposModels;
            return null;

        }
        public async Task<IActionResult> SearchUser(string? search)
        {
            if(search == null)
            {
                search = "pft6fk";
            }   
            var request = new SearchUsersRequest(search);

            var result = await client.Search.SearchUsers(request);
            
            return Json(result.Items);
        }
        public async Task<IActionResult> Details(long id)
        {
            if (IfContributorsExists(id) != null)
            {
                var detailsDb = _unitOfWork.ReposRepository.GetAll();
                var repo = detailsDb.Where(n => n.GitHubId == id).FirstOrDefault();
                var contributors = IfContributorsExists(id);
                var obj = new DetailsDb();
                obj.Repository = repo;
                obj.Contributors = contributors;

                return View("DetailsDb", obj);
            }
            else
            {
                //repo api
                //https://api.github.com/repos/twbs/bootstrap
                var obj = new Details();
                var contributors = await client.Repository.GetAllContributors(id);
                obj.Contributors = contributors;

                obj.Repository = await client.Repository.Get(id);

                foreach (var item in contributors)
                {
                    _unitOfWork.ContributorsRepository.AddToDb(item, id, contributors.Count);
                    _unitOfWork.Save();
                }

                return View(obj);
            }
        }
        public IList<ContributorsModel>?  IfContributorsExists(long repoId)
        {
            var contributorsDb = _unitOfWork.ContributorsRepository.GetAll();

            List<ContributorsModel> Contributors = new List<ContributorsModel>();

            foreach (var item in contributorsDb)
            {
                if(item.RepoId == repoId)
                    Contributors.Add(item);
            }

            if(Contributors.Any())
                return Contributors;

            return null;
        }
        public async Task<IActionResult> UserDetails(string login)
        {
            if (IfUserExists(login) != null)
            {
                var obj = new UserDetailsModel();
                obj.User = IfUserExists(login);
                obj.Repos = TakeUserRepos(login);

                return View("UserDetailsDb", obj);
            }
            else
            {
                var obj = new UserModel();
                //user api
                //https://api.github.com/users/defunkt 

                obj.User = await client.User.Get(login);
                obj.Repositories = await client.Repository.GetAllForUser(login);

                _unitOfWork.UserRepository.AddToDb(obj.User);
                foreach (var item in obj.Repositories)
                {//All public Repos of user
                    _unitOfWork.ReposRepository.AddToDb(item);
                }
                _unitOfWork.Save();

                return View(obj);
            }
        }
        public UserModelNew? IfUserExists(string login)
        {
            IEnumerable<ReposModel> RepoDb = _unitOfWork.ReposRepository.GetAll();
            IEnumerable<UserModelNew> UserDb = _unitOfWork.UserRepository.GetAll();

            var User = (from user in UserDb 
                       where user.Login == login
                       select user).FirstOrDefault();
            
            if(User == null)
                return null;
            return User;
        }
        public IList<ReposModel>? TakeUserRepos(string OwnerLogin)
        {
            IEnumerable<ReposModel> RepoDb = _unitOfWork.ReposRepository.GetAll();
            
            List<ReposModel> reposModels = new List<ReposModel>();
            foreach (var item in RepoDb)
            {
                if (item.GitHubOwnerLogin.Contains(OwnerLogin))
                {
                    reposModels.Add(item);
                }
            }
            if (reposModels.Any())
                return reposModels;
            return null;

        }
    }

}
