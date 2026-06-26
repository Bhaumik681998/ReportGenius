using Microsoft.AspNetCore.Mvc;
using ReportGenius.Application.DTOs.AI;
using ReportGenius.Application.Interfaces;

namespace ReportGenius.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public sealed class AIController : ControllerBase
    {
        private readonly IAIQueryService _aiQueryService;
        public AIController(IAIQueryService aiQueryService)
        {
            _aiQueryService = aiQueryService;
        }

        [HttpPost("generate-sql")]
        public async Task<IActionResult> GenerateSql([FromBody] GenerateSqlRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Prompt is required."
                });
            }

            var response = await _aiQueryService.GenerateQueryAsync(request, cancellationToken);

            return Ok(response);
        }
    }
}
