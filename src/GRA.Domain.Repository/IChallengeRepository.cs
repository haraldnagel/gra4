﻿using GRA.Domain.Model;
using System.Linq;

namespace GRA.Domain.Repository
{
    public interface IChallengeRepository : IRepository<Model.Challenge>
    {
        void AddChallengeTaskType(int userId, string name);
        IQueryable<Challenge> GetPagedChallengeList(int userId, int skip, int take);
    }
}
