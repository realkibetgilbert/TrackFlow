using Microsoft.AspNetCore.SignalR;

namespace Trackflow.API.Hubs
{
    public class LocationHub : Hub
    {
        public Task JoinDeviceGroup(int deviceId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, GetDeviceGroupName(deviceId));
        }

        public Task LeaveDeviceGroup(int deviceId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, GetDeviceGroupName(deviceId));
        }

        public static string GetDeviceGroupName(int deviceId) => $"device-{deviceId}";
    }


}
