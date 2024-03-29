﻿namespace GitHub.Repositories.Repository
{
    public interface IUnitOfWork
    {
        IDetailsRepository DetailsRepository { get; }
        IReposRepository ReposRepository { get; }
        IUserRepository UserRepository { get; }
        IContributorsRepository ContributorsRepository { get; }

        void Save();
    }
}
