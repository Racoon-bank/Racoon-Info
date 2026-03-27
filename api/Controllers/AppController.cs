using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("app")]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get app info (theme and hidden bank accounts)
        /// </summary>
        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> GetAppInfo()
        {
            var info = await _appService.GetAppInfo(User.GetId());
            return Ok(info);
        }

        /// <summary>
        /// Switch theme
        /// </summary>
        [HttpPut("theme")]
        [Authorize]
        public async Task<IActionResult> SwitchTheme()
        {
            var profile = await _appService.SwitchTheme(User.GetId());
            return Ok(profile);
        }

        /// <summary>
        /// Hide bank account
        /// </summary>
        [HttpPost("bankAccount/{id}")]
        [Authorize]
        public async Task<IActionResult> HideBankAccount([FromRoute] Guid id)
        {
            var bankAccount = await _appService.HideBankAccount(id, User.GetId());
            return Ok(bankAccount);
        }

        /// <summary>
        /// Reveal bank account
        /// </summary>
        [HttpDelete("bankAccount/{id}")]
        [Authorize]
        public async Task<IActionResult> RevealBankAccount([FromRoute] Guid id)
        {
            var bankAccount = await _appService.RevealBankAccount(id, User.GetId());
            return Ok(bankAccount);
        }
    }
}