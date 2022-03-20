﻿using GitHub.Data;
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

            tokenAuth = new Credentials("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");
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
            //Define own Repository class -> custom
            // ID->integer, Lang->string, FullName->string
            //DBconnect

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


            //repo api
            //https://api.github.com/repos/twbs/bootstrap
            var obj = new Details();
            obj.Repository = await client.Repository.Get(id);

            var contributors = await client.Repository.GetAllContributors(id);

            obj.Contributors = contributors;

            return View(obj);
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