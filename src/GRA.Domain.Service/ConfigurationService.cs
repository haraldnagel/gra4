﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GRA.Domain.Repository;

namespace GRA.Domain.Service
{
    public class ConfigurationService : Abstract.BaseService<ConfigurationService>
    {
        private readonly ISiteRepository siteRepository;
        private readonly ISystemRepository systemRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IProgramRepository programRepository;
        public ConfigurationService(ILogger<ConfigurationService> logger,
            ISiteRepository siteRepository,
            ISystemRepository systemRepository,
            IBranchRepository branchRepository,
            IProgramRepository programRepository) : base(logger)
        {
            if (siteRepository == null)
            {
                throw new ArgumentNullException(nameof(siteRepository));
            }
            this.siteRepository = siteRepository;

            if (systemRepository == null)
            {
                throw new ArgumentNullException(nameof(systemRepository));
            }
            this.systemRepository = systemRepository;

            if (branchRepository == null)
            {
                throw new ArgumentNullException(nameof(branchRepository));
            }
            this.branchRepository = branchRepository;

            if (programRepository == null)
            {
                throw new ArgumentNullException(nameof(programRepository));
            }
            this.programRepository = programRepository;
        }

        public bool NeedsInitialSetup()
        {
            return siteRepository.GetAll().Count() == 0;
        }

        public void InitialSetup(Model.User adminUser)
        {
            int creatorUserId = 0;
            var allSites = siteRepository.GetAll();

            if (allSites.Count() > 0)
            {
                throw new Exception($"Can't perform initial setup with existing sites: found {allSites.Count()} in the database.");
            }

            var site = new Model.Site
            {
                Name = "Default site",
                Path = "default"
            };
            // create default site
            siteRepository.Add(creatorUserId, site);
            siteRepository.Save();

            site = siteRepository.GetAll().SingleOrDefault();

            if (site == null)
            {
                throw new Exception("Unable to add initial default site or multiple sites found.");
            }

            var system = new Model.System
            {
                SiteId = site.Id,
                Name = "Maricopa County Library District"
            };
            systemRepository.Add(creatorUserId, system);
            systemRepository.Save();

            system = systemRepository.GetAll().SingleOrDefault();

            if (system == null)
            {
                throw new Exception("Unable to add initial default system or multiple systems found.");
            }

            var branch = new Model.Branch
            {
                SiteId = site.Id,
                SystemId = system.Id,
                Name = "Admin",
                Address = "2700 N. Central Ave Ste 700, 85004",
                Telephone = "602-652-3064",
                Url = "http://mcldaz.org/"
            };

            branchRepository.Add(creatorUserId, branch);
            branchRepository.Save();

            var program = new Model.Program
            {
                SiteId = site.Id,
                Achiever = 1000,
                Name = "Winter Reading Program"
            };

            programRepository.Add(creatorUserId, program);
            programRepository.Save();
        }
    }
}