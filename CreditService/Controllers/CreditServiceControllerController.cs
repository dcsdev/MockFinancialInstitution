using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CreditServiceController : ControllerBase
    {
        private readonly ILogger<CreditServiceController> _logger;

        public CreditServiceController(ILogger<CreditServiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("/v1/getCurrentCreditScore")]
        public async Task<IActionResult> GetCurrentCreditScore()
        {
            int[] ficoScores = { 610,650,690,700,713,811 };

            Random random = new Random();
            int randomIndex = random.Next(ficoScores.Length);
            int selectedValue = ficoScores[randomIndex];

            return Ok(new { creditScore = selectedValue });
        }

    }
}