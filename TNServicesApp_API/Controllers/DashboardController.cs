using Microsoft.AspNetCore.Mvc;
using TNServicesApp_API.Models;
using TNServicesApp_API.Repositories.IRepositories;

namespace TNServicesApp_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("sslc-summary")]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                 var response = await _dashboardRepository.GetSslcDataAsync();
                 return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = "An unexpected error occurred while processing the request."
                });
            }
        }

        [HttpGet("hsc-summary")]
        public async Task<IActionResult> GetHscData()
        {
            try
            {
                var response = await _dashboardRepository.GetHscDataAsync();
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = "An unexpected error occurred while processing the request."
                });
            }
        }

        [HttpPost("sslc-insert")]
        public async Task<IActionResult> InsertSslcData([FromBody] StudentsInsertRequest sslcData)
        {
            try
            {
                var response = await _dashboardRepository.SSLCInsertDataAsync(sslcData);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = "An unexpected error occurred while processing the request."
                });
            }
        }

        [HttpPost("hsc-insert")]
        public async Task<IActionResult> InsertHSCData([FromBody] HSC_InsertRequest hscData)
        {
            try
            {
                var response = await _dashboardRepository.HSCInsertDataAsync(hscData);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = "An unexpected error occurred while processing the request."
                });
            }
        }
    }
}
