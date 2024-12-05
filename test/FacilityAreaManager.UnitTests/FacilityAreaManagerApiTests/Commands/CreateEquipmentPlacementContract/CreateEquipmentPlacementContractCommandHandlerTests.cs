using AutoMapper;
using FacilityAreaManagerApi.BackgroundServices;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using Moq;

namespace FacilityAreaManagerApi.Commands.CreateEquipmentPlacementContract.Tests
{
    [TestFixture]
    internal class CreateEquipmentPlacementContractCommandHandlerTests
    {
        private Mock<IFacilityAreaManagerRepository> repositoryMock;
        private Mock<IMapper> mapperMock;
        private Mock<IContractProcessingBackgroundService> backgroundServiceMock;
        private CreateEquipmentPlacementContractCommandHandler handler;
        private CancellationToken cancellationToken;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IFacilityAreaManagerRepository>();
            mapperMock = new Mock<IMapper>();
            backgroundServiceMock = new Mock<IContractProcessingBackgroundService>();

            handler = new CreateEquipmentPlacementContractCommandHandler(repositoryMock.Object, mapperMock.Object, backgroundServiceMock.Object);
            cancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task Handle_ValidRequest_CreatesContractAndLogsMessage()
        {
            // Arrange
            var request = new AddEquipmentPlacementContractRequest
            {
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 10
            };

            var facility = new ProductionFacility
            {
                Code = "Facility1",
                StandardAreaForEquipment = 500
            };

            var equipmentType = new ProcessEquipmentType
            {
                Code = "Type1",
                Area = 20
            };

            var entity = new EquipmentPlacementContract();
            var createdEntity = new EquipmentPlacementContract { Code = "Contract1" };
            var response = new EquipmentPlacementContractResponse { Code = "Contract1" };

            repositoryMock.Setup(repo => repo.GetProductionFacilityByCodeAsync("Facility1", cancellationToken))
                .ReturnsAsync(facility);
            repositoryMock.Setup(repo => repo.GetProcessEquipmentTypeByCodeAsync("Type1", cancellationToken))
                .ReturnsAsync(equipmentType);
            repositoryMock.Setup(repo => repo.CreateEquipmentPlacementContractAsync(entity, cancellationToken))
                .ReturnsAsync(createdEntity);

            mapperMock.Setup(m => m.Map<EquipmentPlacementContract>(request)).Returns(entity);
            mapperMock.Setup(m => m.Map<EquipmentPlacementContractResponse>(createdEntity)).Returns(response);

            // Act
            var result = await handler.Handle(new CreateEquipmentPlacementContractCommand(request), cancellationToken);

            // Assert
            Assert.That(result.Code, Is.EqualTo("Contract1"));

            backgroundServiceMock.Verify(s => s.EnqueueLog("Contract Contract1 has been created."), Times.Once);
        }

        [Test]
        public void Handle_InsufficientFreeArea_ThrowsInvalidOperationException()
        {
            // Arrange
            var request = new AddEquipmentPlacementContractRequest
            {
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 30
            };

            var facility = new ProductionFacility
            {
                Code = "Facility1",
                StandardAreaForEquipment = 500
            };

            var equipmentType = new ProcessEquipmentType
            {
                Code = "Type1",
                Area = 20
            };

            repositoryMock.Setup(repo => repo.GetProductionFacilityByCodeAsync("Facility1", cancellationToken))
                .ReturnsAsync(facility);

            repositoryMock.Setup(repo => repo.GetProcessEquipmentTypeByCodeAsync("Type1", cancellationToken))
                .ReturnsAsync(equipmentType);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(new CreateEquipmentPlacementContractCommand(request), cancellationToken));
        }

        [Test]
        public void Handle_FacilityNotFound_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new AddEquipmentPlacementContractRequest
            {
                ProductionFacilityCode = "NonExistentFacility",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 10
            };

            repositoryMock.Setup(repo => repo.GetProductionFacilityByCodeAsync("NonExistentFacility", cancellationToken))
                .ReturnsAsync((ProductionFacility?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidDataException>(() =>
                handler.Handle(new CreateEquipmentPlacementContractCommand(request), cancellationToken));
        }

        [Test]
        public void Handle_EquipmentTypeNotFound_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new AddEquipmentPlacementContractRequest
            {
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "NonExistentType",
                EquipmentQuantity = 10
            };

            var facility = new ProductionFacility
            {
                Code = "Facility1",
                StandardAreaForEquipment = 500
            };

            repositoryMock.Setup(repo => repo.GetProductionFacilityByCodeAsync("Facility1", cancellationToken))
                .ReturnsAsync(facility);

            repositoryMock.Setup(repo => repo.GetProcessEquipmentTypeByCodeAsync("NonExistentType", cancellationToken))
                .ReturnsAsync((ProcessEquipmentType?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidDataException>(() =>
                handler.Handle(new CreateEquipmentPlacementContractCommand(request), cancellationToken));
        }
    }
}