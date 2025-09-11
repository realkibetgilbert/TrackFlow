using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackflow.API.Application.Features.Location.Interfaces;
using Trackflow.API.Application.Mapping.Loc.Interfaces;

namespace Trackflow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("live")]
        public async Task<IActionResult> GetLatest([FromQuery] int deviceId)
        {
            if (deviceId <= 0)
                return BadRequest("Valid DeviceId is required.");

            var latestLocation = await _locationService.GetLatestLocationAsync(deviceId);

            if (latestLocation == null)
                return NotFound($"No location found for device {deviceId}.");

            return Ok(latestLocation);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int deviceId)
        {
            if (deviceId <= 0)
                return BadRequest("Valid DeviceId is required.");

            var history = await _locationService.GetLocationHistoryAsync(deviceId);
            return Ok(history);
        }

    }
}
