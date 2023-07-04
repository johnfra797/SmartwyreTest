using Microsoft.AspNetCore.Mvc;
using Smartwyre.Data.DTO;
using Smartwyre.DeveloperTest.Services;
using System;

namespace Smartwyre.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmartwyreController : ControllerBase
    {
        private readonly ILogger<SmartwyreController> _logger;
        private IRebateService _rebateService;

        public SmartwyreController(ILogger<SmartwyreController> logger, IRebateService rebateService)
        {
            _logger = logger;
            _rebateService = rebateService;
            _rebateService.PopulateDB();
        }
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation($"Request Get");
            return "Smartwyre API";
        }
        [HttpPost("Calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculateRebateRequest calculateRebateRequest)
        {
            _logger.LogInformation($"Request Calculate");
            var result = _rebateService.Calculate(calculateRebateRequest);
            return Ok(result);
        }
    }
}