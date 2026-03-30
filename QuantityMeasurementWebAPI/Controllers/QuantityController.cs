using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModel.DTOs;
using QuantityMeasurementModel.Common;
using Microsoft.AspNetCore.Authorization;

namespace QuantityMeasurementConsole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityController(IQuantityMeasurementService service)
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
        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityRequestDTO request)
        {
            var result = _service.Add(request.First, request.Second);

            return Ok(new ApiResponse<object>   // ✅ FIXED
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

            return Ok(new ApiResponse<object>   // ✅ FIXED
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