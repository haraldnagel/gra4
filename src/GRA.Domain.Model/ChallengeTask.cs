﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GRA.Domain.Model
{
    public class ChallengeTask : Abstract.BaseDomainEntity
    {
        [Required]
        public int ChallengeId { get; set; }
        [Required]
        public int Position { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Author { get; set; }

        [MaxLength(30)]
        public string Isbn { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        public int ChallengeTaskTypeId { get; set; }

        [Required]
        [DisplayName("Task Type")]
        public Model.ChallengeTaskType ChallengeTaskType { get; set; }
    }
}
