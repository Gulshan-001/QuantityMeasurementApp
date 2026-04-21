using ArithmeticService.Models;
using ArithmeticService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArithmeticService.Controllers
{
    [ApiController]
    [Route("api/Quantity")]
    public class ArithmeticController : ControllerBase
    {
        private readonly IArithmeticLogicService _service;

        public ArithmeticController(IArithmeticLogicService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityRequestDTO request)
        {
            var result = _service.Add(request.First, request.Second);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result,
                Message = "Addition successful"
            });
        }

        [Authorize]
        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] QuantityRequestDTO request)
        {
            var result = _service.Subtract(request.First, request.Second);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result,
                Message = "Subtraction successful"
            });
        }

        [Authorize]
        [HttpPost("divide")]
        public IActionResult Divide([FromBody] QuantityRequestDTO request)
        {
            var result = _service.Divide(request.First, request.Second);
            return Ok(new ApiResponse<double>
            {
                Success = true,
                Data = result,
                Message = "Division successful"
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
