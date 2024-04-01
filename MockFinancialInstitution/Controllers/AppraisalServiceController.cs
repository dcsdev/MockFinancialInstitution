using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClosingConsolidationService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AppraisalServiceController : ControllerBase
    {
        public AppraisalServiceController()
        {
        }

        [HttpGet, Route("/v1/getCurrentAppraisalValue")]
        public async Task<IActionResult> GetCurrentAppraisalValue()
        {
            string[] homeValues = { "$119342.00", "$906875.00", "$910326.00", "$752443.00", "$768870.00" };

            Random random = new Random();
            int randomIndex = random.Next(homeValues.Length);
            string selectedValue = homeValues[randomIndex];

            return Ok(new { appraisal = selectedValue });
        }

        
    }
}