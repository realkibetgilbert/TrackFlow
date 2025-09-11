using Microsoft.AspNetCore.SignalR;
using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Application.Interfaces;
using Trackflow.API.Hubs;

namespace Trackflow.API.Services
{
    public class SignalRLocationNotifier : ILocationNotifier
    {
        private readonly IHubContext<LocationHub> _hubContext;

        public SignalRLocationNotifier(IHubContext<LocationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyLocationAsync(LocationToDisplayDto locationToDisplayDto)
        {

            var groupName = LocationHub.GetDeviceGroupName(locationToDisplayDto.DeviceId);
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveLocation", locationToDisplayDto);
        }

    }
}
