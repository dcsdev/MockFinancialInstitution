using Microsoft.AspNetCore.Mvc;
using MockFinancialInstitutionWeb.Api_Clients;
using MockFinancialInstitutionWeb.Models.ViewModels;

namespace MockFinancialInstitutionWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClosingCoordinatorClient _closingCoordinatorClient;

        public HomeController(ILogger<HomeController> logger, ClosingCoordinatorClient closingCoordinatorClient)
        {
            _logger = logger;
            _closingCoordinatorClient = closingCoordinatorClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var viewModel = await GetHomeInfoViewModelAsync();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving home info.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var fileUploadResult = await _closingCoordinatorClient.UploadDocument(file);
                var viewModel = await GetHomeInfoViewModelAsync();
                viewModel.DocumentUploadResult = fileUploadResult;
                
                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the file.");
                return View("Error");
            }
        }

        private async Task<HomeInfoViewModel> GetHomeInfoViewModelAsync()
        {
            var healthCheckTasks = new[]
            {
                _closingCoordinatorClient.HealthCheckV1(),
                _closingCoordinatorClient.HealthCheckV2(),
                _closingCoordinatorClient.HealthCheckV3()
            };

            await Task.WhenAll(healthCheckTasks);

            var result = await _closingCoordinatorClient.RetreiveCustomerFile();

            return new HomeInfoViewModel
            {
                HomeAppraisalValue = result.HomeAppraisalValue,
                CreditScore = result.CreditScore,
                HealthCheckV1 = $"Online: {healthCheckTasks[0].Result.status}",
                HealthCheckV2 = $"Online: {healthCheckTasks[1].Result.status}\nTimestamp: {healthCheckTasks[1].Result.timestamp}\n",
                HealthCheckV3 = $"Online: {healthCheckTasks[2].Result.status}\nUptime: {healthCheckTasks[2].Result.Uptime}\n Memory Usage: {healthCheckTasks[2].Result.MemoryUsage}"
            };
        }
    }
}
