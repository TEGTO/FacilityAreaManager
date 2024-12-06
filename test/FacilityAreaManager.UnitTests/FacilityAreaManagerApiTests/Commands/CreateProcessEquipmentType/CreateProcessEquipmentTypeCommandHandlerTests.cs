using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using Moq;

namespace FacilityAreaManagerApi.Commands.CreateProcessEquipmentType.Tests
{
    [TestFixture]
    internal class CreateProcessEquipmentTypeCommandHandlerTests
    {
        private Mock<IFacilityAreaManagerRepository> repositoryMock;
        private Mock<IMapper> mapperMock;
        private CreateProcessEquipmentTypeCommandHandler handler;
        private CancellationToken cancellationToken;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IFacilityAreaManagerRepository>();
            mapperMock = new Mock<IMapper>();

            handler = new CreateProcessEquipmentTypeCommandHandler(repositoryMock.Object, mapperMock.Object);
            cancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task Handle_ValidRequest_CreatesProcessEquipmentTypeAndReturnsResponse()
        {
            // Arrange
            var request = new AddProcessEquipmentTypeRequest
            {
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            var command = new CreateProcessEquipmentTypeCommand(request);

            var entity = new ProcessEquipmentType
            {
                Code = "Type1",
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            var createdEntity = new ProcessEquipmentType
            {
                Code = "Type1",
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            var response = new ProcessEquipmentTypeResponse
            {
                Code = "Type1",
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            mapperMock.Setup(m => m.Map<ProcessEquipmentType>(request)).Returns(entity);
            repositoryMock.Setup(r => r.CreateProcessEquipmentTypeAsync(entity, cancellationToken)).ReturnsAsync(createdEntity);
            mapperMock.Setup(m => m.Map<ProcessEquipmentTypeResponse>(createdEntity)).Returns(response);

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Code, Is.EqualTo("Type1"));
            Assert.That(result.Name, Is.EqualTo("EquipmentTypeA"));
            Assert.That(result.Area, Is.EqualTo(50.0f));

            mapperMock.Verify(m => m.Map<ProcessEquipmentType>(request), Times.Once);
            repositoryMock.Verify(r => r.CreateProcessEquipmentTypeAsync(entity, cancellationToken), Times.Once);
            mapperMock.Verify(m => m.Map<ProcessEquipmentTypeResponse>(createdEntity), Times.Once);
        }

        [Test]
        public void Handle_NullCreatedEntity_ThrowsInvalidOperationException()
        {
            // Arrange
            var request = new AddProcessEquipmentTypeRequest
            {
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            var command = new CreateProcessEquipmentTypeCommand(request);

            var entity = new ProcessEquipmentType
            {
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            mapperMock.Setup(m => m.Map<ProcessEquipmentType>(request)).Returns(entity);
            repositoryMock.Setup(r => r.CreateProcessEquipmentTypeAsync(entity, cancellationToken)).ReturnsAsync((ProcessEquipmentType?)null!);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, cancellationToken));
        }
    }
}