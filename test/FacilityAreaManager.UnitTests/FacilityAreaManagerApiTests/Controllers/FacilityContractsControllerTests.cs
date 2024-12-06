using FacilityAreaManagerApi.Commands.CreateEquipmentPlacementContract;
using FacilityAreaManagerApi.Commands.CreateProcessEquipmentType;
using FacilityAreaManagerApi.Commands.CreateProductionFacility;
using FacilityAreaManagerApi.Commands.GetAllEquipmentPlacementContracts;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FacilityAreaManagerApi.Controllers.Tests
{
    [TestFixture]
    internal class FacilityContractsControllerTests
    {
        private Mock<IMediator> mediatorMock;
        private FacilityContractsController controller;
        private CancellationToken cancellationToken;

        [SetUp]
        public void SetUp()
        {
            mediatorMock = new Mock<IMediator>();

            controller = new FacilityContractsController(mediatorMock.Object);
            cancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task CreateEquipmentPlacementContract_ValidRequest_ReturnsOkWithResponse()
        {
            // Arrange
            var request = new AddEquipmentPlacementContractRequest
            {
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 10
            };

            var expectedResponse = new EquipmentPlacementContractResponse
            {
                Code = "Contract1",
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 10
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateEquipmentPlacementContractCommand>(), cancellationToken))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.CreateEquipmentPlacementContract(request, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(expectedResponse));

            mediatorMock.Verify(m => m.Send(It.Is<CreateEquipmentPlacementContractCommand>(c => c.Request == request), cancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateProcessEquipmentType_ValidRequest_ReturnsOkWithResponse()
        {
            // Arrange
            var request = new AddProcessEquipmentTypeRequest
            {
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            var expectedResponse = new ProcessEquipmentTypeResponse
            {
                Code = "Type1",
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateProcessEquipmentTypeCommand>(), cancellationToken))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.CreateProcessEquipmentType(request, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(expectedResponse));

            mediatorMock.Verify(m => m.Send(It.Is<CreateProcessEquipmentTypeCommand>(c => c.Request == request), cancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateProductionFacility_ValidRequest_ReturnsOkWithResponse()
        {
            // Arrange
            var request = new AddProductionFacilityRequest
            {
                Name = "FacilityA",
                StandardAreaForEquipment = 1000.0f
            };

            var expectedResponse = new ProductionFacilityResponse
            {
                Code = "Facility1",
                Name = "FacilityA",
                StandardAreaForEquipment = 1000.0f
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductionFacilityCommand>(), cancellationToken))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.CreateProductionFacility(request, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(expectedResponse));

            mediatorMock.Verify(m => m.Send(It.Is<CreateProductionFacilityCommand>(c => c.Request == request), cancellationToken), Times.Once);
        }

        [Test]
        public async Task GetAllEquipmentPlacementContracts_ReturnsOkWithList()
        {
            // Arrange
            var expectedResponse = new List<EquipmentPlacementContractResponse>
            {
                new EquipmentPlacementContractResponse
                {
                    Code = "Contract1",
                    ProductionFacilityCode = "Facility1",
                    ProcessEquipmentTypeCode = "Type1",
                    EquipmentQuantity = 10
                },
                new EquipmentPlacementContractResponse
                {
                    Code = "Contract2",
                    ProductionFacilityCode = "Facility2",
                    ProcessEquipmentTypeCode = "Type2",
                    EquipmentQuantity = 20
                }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllEquipmentPlacementContractsQuery>(), cancellationToken))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetAllEquipmentPlacementContracts(cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(okResult.Value, Is.EqualTo(expectedResponse));

            mediatorMock.Verify(m => m.Send(It.IsAny<GetAllEquipmentPlacementContractsQuery>(), cancellationToken), Times.Once);
        }
    }
}