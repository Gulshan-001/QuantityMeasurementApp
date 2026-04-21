using CompareService.Models;
using CompareService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompareService.Controllers
{
    [ApiController]
    [Route("api/Quantity")]
    public class CompareController : ControllerBase
    {
        private readonly ICompareLogicService _service;

        public CompareController(ICompareLogicService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] QuantityRequestDTO request)
        {
            var result = _service.CompareEquality(request.First, request.Second);
            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Data = result,
                Message = "Comparison successful"
            });
        }

        [Authorize]
        [HttpGet("history")]
        public IActionResult GetAllHistory()
        {
            var data = _service.GetAllHistory();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = data,
                Message = "History fetched successfully"
            });
        }

        [Authorize]
        [HttpGet("history/operation/{operation}")]
        public IActionResult GetByOperation(string operation)
        {
            var data = _service.GetByOperation(operation);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = data,
                Message = $"History for '{operation}' fetched"
            });
        }

        [Authorize]
        [HttpGet("count/{operation}")]
        public IActionResult GetCount(string operation)
        {
            var count = _service.GetOperationCount(operation);
            return Ok(new ApiResponse<int>
            {
                Success = true,
                Data = count,
                Message = $"Count for '{operation}' fetched"
            });
        }
    }
}
