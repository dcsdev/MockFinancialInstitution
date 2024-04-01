using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockFinancialInstitutionWeb.Api_Clients;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace ClosingConsolidationService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClosingCoordinatorController : ControllerBase
    {
        private readonly ILogger<ClosingCoordinatorController> _logger;
        private readonly AppraisalServiceClient _appraisalServiceClient;
        private readonly CreditServiceClient _creditServiceClient;

        public ClosingCoordinatorController(ILogger<ClosingCoordinatorController> logger, CreditServiceClient creditServiceClient, AppraisalServiceClient appraisalServiceClient)
        {
            _logger = logger;
            _creditServiceClient = creditServiceClient;
            _appraisalServiceClient = appraisalServiceClient;
        }

        [HttpGet, Route("/v1/healthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(new { Status = "Online"});
        }

        [HttpGet, Route("/v2/healthCheck")]
        public IActionResult HealthCheckV2()
        {
            return Ok(new { Status = "Online", Timestamp = DateTime.UtcNow });
        }

        [HttpGet, Route("/v3/healthCheck")]
        public IActionResult HealthCheckV3()
        {
            var report = new
            {
                Status = "Online",
                Uptime = Environment.TickCount64,
                MemoryUsage = GC.GetTotalMemory(forceFullCollection: false)
            };

            return Ok(report);
        }

        [HttpGet, Route("/v1/retrieveCustomerFile")]
        public async Task<IActionResult> RetreiveCustomerFile()
        {
            var appraisalValue = await _appraisalServiceClient.GetCurrentAppraisalValue();
            var creditScoreValue = await _creditServiceClient.GetCurrentCreditScore();
            
            return Ok(new { appraisal = appraisalValue, creditscore = creditScoreValue });
        }

        [HttpPost, Route("/v1/postDocument")]
        public async Task<IActionResult> PostDocument(IFormFile file)
        {
            var result = _appraisalServiceClient.UploadDocument();
            return Ok($"Document Has Been Uploaded...");
        }
    }
}