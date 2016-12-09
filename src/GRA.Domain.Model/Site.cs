﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GRA.Domain.Model
{
    public class Site : Abstract.BaseDomainEntity
    {
        [Required]
        [MaxLength(255)]
        public string Path { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string PageTitle { get; set; }
        [MaxLength(255)]
        public string Domain { get; set; }
        public bool IsDefault { get; set; }
        public string Footer { get; set; }

        public DateTime? RegistrationOpens { get; set; }
        public DateTime? ProgramStarts { get; set; }
        public DateTime? ProgramEnds { get; set; }
        public DateTime? AccessClosed { get; set; }

        public string OutgoingMailHost { get; set; }
        public int? OutgoingMailPort { get; set; }
        public string OutgoingMailLogin { get; set; }
        public string OutgoingMailPassword { get; set; }

        public string ExternalEventListUrl { get; set; }
    }
}
