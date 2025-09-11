using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Trackflow.API.Application.DTOs.Location;
using Trackflow.API.Application.Features.Location.Interfaces;
using Trackflow.API.Application.Features.Location.Services;
using Trackflow.API.Application.Mapping.Loc.Interfaces;
using Trackflow.API.Core.Entities;
using Xunit;

namespace Trackflow.API.Application.Tests.Features.Location.Services
{
    public class LocationServiceTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly Mock<ILocationMapper> _locationMapperMock;
        private readonly LocationService _locationService;

        public LocationServiceTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _locationMapperMock = new Mock<ILocationMapper>();
            _locationService = new LocationService(_locationRepositoryMock.Object, _locationMapperMock.Object);
        }

        [Fact]
        public async Task GetLatestLocationAsync_ReturnsMappedDto_WhenLocationExists()
        {
            // Arrange
            int deviceId = 1;
            var location = new Location
            {
                Id = 10,
                DeviceId = deviceId,
                Latitude = 12.34,
                Longitude = 56.78,
                Timestamp = DateTime.UtcNow
            };
            var dto = new LocationToDisplayDto
            {
                DeviceId = deviceId,
                Latitude = 12.34,
                Longitude = 56.78,
                Timestamp = location.Timestamp
            };

            _locationRepositoryMock.Setup(r => r.GetLatestLocationAsync(deviceId))
                .ReturnsAsync(location);
            _locationMapperMock.Setup(m => m.ToDisplay(location))
                .Returns(dto);

            // Act
            var result = await _locationService.GetLatestLocationAsync(deviceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.DeviceId, result.DeviceId);
            Assert.Equal(dto.Latitude, result.Latitude);
            Assert.Equal(dto.Longitude, result.Longitude);
            Assert.Equal(dto.Timestamp, result.Timestamp);
        }

        [Fact]
        public async Task GetLatestLocationAsync_ReturnsNull_WhenNoLocationExists()
        {
            // Arrange
            int deviceId = 2;
            _locationRepositoryMock.Setup(r => r.GetLatestLocationAsync(deviceId))
                .ReturnsAsync((Location?)null);

            // Act
            var result = await _locationService.GetLatestLocationAsync(deviceId);

            // Assert
            Assert.Null(result);
            _locationMapperMock.Verify(m => m.ToDisplay(It.IsAny<Location>()), Times.Never);
        }

        [Fact]
        public async Task GetLocationHistoryAsync_ReturnsMappedDtos_WhenLocationsExist()
        {
            // Arrange
            int deviceId = 3;
            var locations = new List<Location>
            {
                new Location { Id = 1, DeviceId = deviceId, Latitude = 1, Longitude = 2, Timestamp = DateTime.UtcNow },
                new Location { Id = 2, DeviceId = deviceId, Latitude = 3, Longitude = 4, Timestamp = DateTime.UtcNow }
            };
            var dtos = new List<LocationToDisplayDto>
            {
                new LocationToDisplayDto { DeviceId = deviceId, Latitude = 1, Longitude = 2, Timestamp = locations[0].Timestamp },
                new LocationToDisplayDto { DeviceId = deviceId, Latitude = 3, Longitude = 4, Timestamp = locations[1].Timestamp }
            };

            _locationRepositoryMock.Setup(r => r.GetLocationHistoryAsync(deviceId))
                .ReturnsAsync(locations);
            _locationMapperMock.Setup(m => m.ToDisplay(locations))
                .Returns(dtos);

            // Act
            var result = await _locationService.GetLocationHistoryAsync(deviceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(dtos[0].Latitude, result[0].Latitude);
            Assert.Equal(dtos[1].Longitude, result[1].Longitude);
        }

        [Fact]
        public async Task GetLocationHistoryAsync_ReturnsEmptyList_WhenMapperReturnsNull()
        {
            // Arrange
            int deviceId = 4;
            var locations = new List<Location>
            {
                new Location { Id = 1, DeviceId = deviceId, Latitude = 1, Longitude = 2, Timestamp = DateTime.UtcNow }
            };

            _locationRepositoryMock.Setup(r => r.GetLocationHistoryAsync(deviceId))
                .ReturnsAsync(locations);
            _locationMapperMock.Setup(m => m.ToDisplay(locations))
                .Returns((List<LocationToDisplayDto>?)null);

            // Act
            var result = await _locationService.GetLocationHistoryAsync(deviceId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
