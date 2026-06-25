using Microsoft.AspNetCore.Mvc;
using ReportGenius.Application.DTOs.AI;
using ReportGenius.Application.Interfaces;

namespace ReportGenius.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public sealed class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("generate-sql")]
        public async Task<IActionResult> GenerateSql([FromBody] GenerateSqlRequest request,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Prompt is required."
                });
            }

            var sql = await _aiService.GenerateSqlAsync(request.Prompt,cancellationToken);

            return Ok(new GenerateSqlResponse
            {
                Success = true,
                Sql = sql
            });
        }
    }
}
