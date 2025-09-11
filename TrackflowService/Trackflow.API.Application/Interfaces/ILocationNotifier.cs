using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackflow.API.Application.DTOs.Location;

namespace Trackflow.API.Application.Interfaces
{
    public interface ILocationNotifier
    {
        Task NotifyLocationAsync(LocationToDisplayDto locationToDisplayDto);
    }
}
