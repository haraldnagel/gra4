﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using GRA.Domain.Repository;
using GRA.Domain.Model;
using System.Security.Claims;

namespace GRA.Domain.Service
{
    public class ChallengeService : Abstract.BaseService<ChallengeService>
    {
        private readonly IChallengeRepository challengeRepository;
        private readonly IChallengeTaskRepository challengeTaskRepository;

        public ChallengeService(ILogger<ChallengeService> logger,
            IChallengeRepository challengeRepository,
            IChallengeTaskRepository challengeTaskRepository) : base(logger)
        {
            if (challengeRepository == null)
            {
                throw new ArgumentNullException(nameof(challengeRepository));
            }
            this.challengeRepository = challengeRepository;
            if (challengeTaskRepository == null)
            {
                throw new ArgumentNullException(nameof(challengeTaskRepository));
            }
            this.challengeTaskRepository = challengeTaskRepository;
        }

        /// <summary>
        /// A paginated list of challenges which are visible to the provided user
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="skip">The number of elements to skip before returning the remaining elements</param>
        /// <param name="take">The number of elements to return</param>
        /// <returns><see cref="DataWithCount{DataType}"/> containing the challenges and the total challenge count</returns>
        public DataWithCount<IEnumerable<Challenge>> GetPaginatedChallengeList(ClaimsIdentity user,
            int skip,
            int take)
        {
            // todo: fix user id
            // todo: add access control - only view authorized challenges
            return new DataWithCount<IEnumerable<Challenge>>
            {
                Data = challengeRepository.PageAll(skip, take),
                Count = challengeRepository.GetChallengeCount()
            };
        }

        /// <summary>
        /// Details on a specific challenge if it's visible to the provided user
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="challengeId">A challenge id</param>
        /// <returns>Details for the requested challenge</returns>
        public Challenge GetChallengeDetails(ClaimsIdentity user, int challengeId)
        {
            // todo: fix user id
            // todo: add access control - only view authorized challenges
            return challengeRepository.GetById(challengeId);
        }

        /// <summary>
        /// Create a new challenge if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="challenge">A populated challenge object</param>
        /// <returns>The challenge which was added with the Id property populated</returns>
        public Challenge AddChallenge(ClaimsIdentity user, Challenge challenge)
        {
            // todo: fix user id
            // todo: add access control - only some users can add
            return challengeRepository.AddSave(GetUserId(user), challenge);
        }

        /// <summary>
        /// Edit an existing challenge if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="challenge">The modified challenge object</param>
        /// <returns>The updated challenge</returns>
        public Challenge EditChallenge(ClaimsIdentity user, Challenge challenge)
        {
            // todo: fix user id
            // todo: add access control - only some users can edit
            return challengeRepository.UpdateSave(GetUserId(user), challenge);
        }

        /// <summary>
        /// Remove an existing challenge if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="challenge">The id of the challenge to remove</param>
        public void RemoveChallenge(ClaimsIdentity user, int challengeId)
        {
            // todo: fix user id
            // todo: add access control - only some users can remove
            challengeRepository.RemoveSave(GetUserId(user), challengeId);
        }

        /// <summary>
        /// Create a new task if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="task">The task to add to the challenge</param>
        /// <param name="challengeId">The id of the challenge to add the task to</param>
        public ChallengeTask AddTask(ClaimsIdentity user, ChallengeTask task)
        {
            // todo: fix user id
            return challengeTaskRepository.AddSave(GetUserId(user), task);
        }

        /// <summary>
        /// Edit an existing task if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="task">The modified task object</param>
        public ChallengeTask EditTask(ClaimsIdentity user, ChallengeTask task)
        {
            // todo: fix user id
            return challengeTaskRepository.UpdateSave(GetUserId(user), task);
        }

        /// <summary>
        /// Get an existing task by id if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="task">TThe id of the task to return</param>
        public ChallengeTask GetTask(ClaimsIdentity user, int id)
        {
            // todo: fix user id
            return challengeTaskRepository.GetById(id);
        }

        /// <summary>
        /// Remove an existing task if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="taskId">The id of the task to remove</param>
        public void RemoveTask(ClaimsIdentity user, int taskId)
        {
            // todo: fix user id
            challengeTaskRepository.RemoveSave(GetUserId(user), taskId);
        }

        /// <summary>
        /// Decrease the sorting position of the task if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="taskId">The id of the task whose position to decrease</param>
        public void DecreaseTaskPosition(ClaimsIdentity user, int taskId)
        {
            // todo: fix user id
            challengeTaskRepository.DecreasePosition(taskId);
        }

        /// <summary>
        /// Increase the sorting position of the task if the provided user has rights
        /// </summary>
        /// <param name="user">A valid user</param>
        /// <param name="taskId">The id of the task whose position to increase</param>
        public void IncreaseTaskPosition(ClaimsIdentity user, int taskId)
        {
            // todo: fix user id
            challengeTaskRepository.IncreasePosition(taskId);
        }

        public IEnumerable<ChallengeTask> GetChallengeTasks(int challengeId)
        {
            return challengeRepository.GetChallengeTasks(challengeId);
        }

    }
}