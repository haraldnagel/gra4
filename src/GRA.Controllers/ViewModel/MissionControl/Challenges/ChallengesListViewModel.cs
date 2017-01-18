﻿using GRA.Controllers.ViewModel.Shared;
using System.Collections.Generic;

namespace GRA.Controllers.ViewModel.MissionControl.Challenges
{
    public class ChallengesListViewModel
    {
        public IEnumerable<GRA.Domain.Model.Challenge> Challenges { get; set; }
        public PaginateViewModel PaginateModel { get; set; }
        public string Search { get; set; }
        public string FilterBy { get; set; }
        public int AllCount { get; set; }
        public int SystemCount { get; set; }
        public int BranchCount { get; set; }
        public int MineCount { get; set; }
        public bool CanAddChallenges { get; set; }
        public bool CanDeleteChallenges { get; set; }
        public bool CanEditChallenges { get; set; }
    }
}
