using Microsoft.AspNetCore.Mvc;
using ReportGenius.Application.Services;

namespace ReportGenius.Controllers
{
    [ApiController]
    [Route("api/database")]
    public sealed class DatabaseController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public DatabaseController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var result = await _databaseService.TestConnectionAsync();

            return Ok(new
            {
                Success = true,
                Message = "Database connection successful.",
                Result = result
            });
        }
    }
}
