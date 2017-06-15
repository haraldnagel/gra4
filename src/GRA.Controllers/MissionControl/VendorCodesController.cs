﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRA.Controllers.ServiceFacade;
using GRA.Domain.Model;
using GRA.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GRA.Controllers.MissionControl
{
    [Area("MissionControl")]
    [Authorize(Policy = Policy.ManageVendorCodes)]
    public class VendorCodesController : Base.MCController
    {
        private readonly VendorCodeService _vendorCodeService;
        public VendorCodesController(ServiceFacade.Controller context,
            VendorCodeService vendorCodeService)
            : base(context)
        {
            _vendorCodeService = vendorCodeService
                ?? throw new ArgumentNullException(nameof(vendorCodeService));
            PageTitle = "Vendor Codes";
        }

        [HttpGet]
        public async Task<IActionResult> ImportStatus()
        {
            var codeTypes = await _vendorCodeService.GetTypeAllAsync();
            var codeTypeSelectList = codeTypes.Select(_ => new SelectListItem
            {
                Value = _.Id.ToString(),
                Text = _.Description

            });
            return View(codeTypeSelectList);
        }

        [HttpPost]
        public async Task<IActionResult> ImportStatus(int vendorCodeId,
            Microsoft.AspNetCore.Http.IFormFile excelFile)
        {
            if (excelFile == null
                || Path.GetExtension(excelFile.FileName).ToLower() != ".xls")
            {
                AlertDanger = "You must select an .xls file.";
                ModelState.AddModelError("excelFile", "You must select an .xls file.");
            }

            if (ModelState.ErrorCount == 0)
            {
                using (var stream = excelFile.OpenReadStream())
                {
                    (ImportStatus status, string message)
                        = await _vendorCodeService.UpdateStatusFromExcel(stream);

                    switch (status)
                    {
                        case Domain.Model.ImportStatus.Success:
                            AlertSuccess = message;
                            break;
                        case Domain.Model.ImportStatus.Info:
                            AlertInfo = message;
                            break;
                        case Domain.Model.ImportStatus.Warning:
                            AlertWarning = message;
                            break;
                        case Domain.Model.ImportStatus.Danger:
                            AlertDanger = message;
                            break;
                    }
                }
            }
            return RedirectToAction("ImportStatus");
        }
    }
}
