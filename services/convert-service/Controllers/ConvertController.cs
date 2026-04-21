using ConvertService.Models;
using ConvertService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConvertService.Controllers
{
    [ApiController]
    [Route("api/Quantity")]
    public class ConvertController : ControllerBase
    {
        private readonly IConvertLogicService _service;

        public ConvertController(IConvertLogicService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("convert")]
        public IActionResult Convert([FromBody] QuantityRequestDTO request)
        {
            var result = _service.Convert(request.First, request.TargetUnit!);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result,
                Message = "Conversion successful"
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
