using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using Moq;

namespace FacilityAreaManagerApi.Commands.GetAllEquipmentPlacementContracts.Tests
{
    [TestFixture]
    internal class GetAllEquipmentPlacementContractsQueryHandlerTests
    {
        private Mock<IFacilityAreaManagerRepository> repositoryMock;
        private Mock<IMapper> mapperMock;
        private GetAllEquipmentPlacementContractsQueryHandler handler;
        private CancellationToken cancellationToken;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IFacilityAreaManagerRepository>();
            mapperMock = new Mock<IMapper>();
            handler = new GetAllEquipmentPlacementContractsQueryHandler(repositoryMock.Object, mapperMock.Object);
            cancellationToken = CancellationToken.None;
        }

        private static IEnumerable<TestCaseData> GetContractsTestCases()
        {
            yield return new TestCaseData(
                new List<EquipmentPlacementContract>
                {
                new EquipmentPlacementContract
                {
                    Code = "Contract1",
                    ProductionFacilityCode = "Facility1",
                    ProcessEquipmentTypeCode = "Type1",
                    EquipmentQuantity = 10
                },
                new EquipmentPlacementContract
                {
                    Code = "Contract2",
                    ProductionFacilityCode = "Facility2",
                    ProcessEquipmentTypeCode = "Type2",
                    EquipmentQuantity = 20
                }
                },
                new List<EquipmentPlacementContractResponse>
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
                },
                2
            ).SetDescription("Multiple contracts should return correctly mapped responses.");

            yield return new TestCaseData(
                new List<EquipmentPlacementContract>(),
                new List<EquipmentPlacementContractResponse>(),
                0
            ).SetDescription("No contracts should return an empty list.");
        }

        [Test]
        [TestCaseSource(nameof(GetContractsTestCases))]
        public async Task Handle_GetContractsTestCases(
            List<EquipmentPlacementContract> contracts,
            List<EquipmentPlacementContractResponse> expectedResponses,
            int expectedCount)
        {
            // Arrange
            repositoryMock.Setup(r => r.GetEquipmentPlacementContractsAsync(cancellationToken))
                .ReturnsAsync(contracts);

            foreach (var contract in contracts)
            {
                var response = expectedResponses.Find(r => r.Code == contract.Code);
                if (response != null)
                {
                    mapperMock.Setup(m => m.Map<EquipmentPlacementContractResponse>(contract)).Returns(response);
                }
            }

            // Act
            var result = await handler.Handle(new GetAllEquipmentPlacementContractsQuery(), cancellationToken);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(expectedCount));

            for (int i = 0; i < expectedResponses.Count; i++)
            {
                Assert.That(result.ElementAt(i).Code, Is.EqualTo(expectedResponses[i].Code));
                Assert.That(result.ElementAt(i).ProductionFacilityCode, Is.EqualTo(expectedResponses[i].ProductionFacilityCode));
                Assert.That(result.ElementAt(i).ProcessEquipmentTypeCode, Is.EqualTo(expectedResponses[i].ProcessEquipmentTypeCode));
                Assert.That(result.ElementAt(i).EquipmentQuantity, Is.EqualTo(expectedResponses[i].EquipmentQuantity));
            }

            repositoryMock.Verify(r => r.GetEquipmentPlacementContractsAsync(cancellationToken), Times.AtLeastOnce);

            foreach (var contract in contracts)
            {
                mapperMock.Verify(m => m.Map<EquipmentPlacementContractResponse>(contract), Times.AtLeastOnce);
            }
        }
    }
}