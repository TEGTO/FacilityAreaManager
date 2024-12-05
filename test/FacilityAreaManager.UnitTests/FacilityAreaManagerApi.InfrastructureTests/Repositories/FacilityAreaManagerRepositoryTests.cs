using DatabaseControl.Repositories;
using FacilityAreaManagerApi.Infrastructure.Data;
using FacilityAreaManagerApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace FacilityAreaManagerApi.Infrastructure.Repositories.Tests
{
    [TestFixture]
    internal class FacilityAreaManagerRepositoryTests
    {
        private Mock<IDatabaseRepository<FacilityAreaManagerDbContext>> repositoryMock;
        private FacilityAreaManagerRepository repository;
        private CancellationToken cancellationToken;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IDatabaseRepository<FacilityAreaManagerDbContext>>();
            repository = new FacilityAreaManagerRepository(repositoryMock.Object);
            cancellationToken = new CancellationToken();
        }

        private static Mock<DbSet<T>> GetDbSetMock<T>(List<T> data) where T : class
        {
            return data.AsQueryable().BuildMockDbSet();
        }

        [Test]
        [TestCase("Facility1", 3, Description = "Creates a valid EquipmentPlacementContract.")]
        public async Task CreateEquipmentPlacementContractAsync_ValidEntity_ReturnsCreatedContract(string facilityCode, int quantity)
        {
            // Arrange
            var contract = new EquipmentPlacementContract
            {
                ProductionFacilityCode = facilityCode,
                EquipmentQuantity = quantity
            };

            repositoryMock.Setup(repo => repo.AddAsync(contract, cancellationToken))
                .ReturnsAsync(contract);

            // Act
            var result = await repository.CreateEquipmentPlacementContractAsync(contract, cancellationToken);

            // Assert
            Assert.That(result.ProductionFacilityCode, Is.EqualTo(facilityCode));
            Assert.That(result.EquipmentQuantity, Is.EqualTo(quantity));
            repositoryMock.Verify(repo => repo.AddAsync(contract, cancellationToken), Times.Once);
        }

        [Test]
        [TestCase("Type1", 50, Description = "Creates a valid ProcessEquipmentType.")]
        public async Task CreateProcessEquipmentTypeAsync_ValidEntity_ReturnsCreatedType(string typeCode, float area)
        {
            // Arrange
            var type = new ProcessEquipmentType
            {
                Code = typeCode,
                Area = area
            };

            repositoryMock.Setup(repo => repo.AddAsync(type, cancellationToken))
                .ReturnsAsync(type);

            // Act
            var result = await repository.CreateProcessEquipmentTypeAsync(type, cancellationToken);

            // Assert
            Assert.That(result.Code, Is.EqualTo(typeCode));
            Assert.That(result.Area, Is.EqualTo(area));
            repositoryMock.Verify(repo => repo.AddAsync(type, cancellationToken), Times.Once);
        }

        [Test]
        [TestCase("Facility1", "Factory A", 300, Description = "Creates a valid ProductionFacility.")]
        public async Task CreateProductionFacilityAsync_ValidEntity_ReturnsCreatedFacility(string code, string name, float standardArea)
        {
            // Arrange
            var facility = new ProductionFacility
            {
                Code = code,
                Name = name,
                StandardAreaForEquipment = standardArea
            };

            repositoryMock.Setup(repo => repo.AddAsync(facility, cancellationToken))
                .ReturnsAsync(facility);

            // Act
            var result = await repository.CreateProductionFacilityAsync(facility, cancellationToken);

            // Assert
            Assert.That(result.Code, Is.EqualTo(code));
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.StandardAreaForEquipment, Is.EqualTo(standardArea));
            repositoryMock.Verify(repo => repo.AddAsync(facility, cancellationToken), Times.Once);
        }

        [Test]
        [TestCase("Type1", true, Description = "Finds a ProcessEquipmentType by code.")]
        [TestCase("TypeX", false, Description = "Does not find a ProcessEquipmentType with invalid code.")]
        public async Task GetProcessEquipmentTypeByCodeAsync_TestCases(string code, bool shouldExist)
        {
            // Arrange
            var types = new List<ProcessEquipmentType>
            {
                new ProcessEquipmentType { Code = "Type1", Name = "Type A" },
                new ProcessEquipmentType { Code = "Type2", Name = "Type B" }
            };

            var dbSetMock = GetDbSetMock(types);
            repositoryMock.Setup(repo => repo.GetQueryableAsync<ProcessEquipmentType>(cancellationToken))
                .ReturnsAsync(dbSetMock.Object);

            // Act
            var result = await repository.GetProcessEquipmentTypeByCodeAsync(code, cancellationToken);

            // Assert
            if (shouldExist)
            {
                Assert.IsNotNull(result);
                Assert.That(result!.Code, Is.EqualTo(code));
            }
            else
            {
                Assert.IsNull(result);
            }

            repositoryMock.Verify(repo => repo.GetQueryableAsync<ProcessEquipmentType>(cancellationToken), Times.Once);
        }

        [Test]
        [TestCase("Facility1", true, Description = "Finds a ProductionFacility by code.")]
        [TestCase("FacilityX", false, Description = "Does not find a ProductionFacility with invalid code.")]
        public async Task GetProductionFacilityByCodeAsync_TestCases(string code, bool shouldExist)
        {
            // Arrange
            var facilities = new List<ProductionFacility>
            {
                new ProductionFacility { Code = "Facility1", Name = "Factory A" },
                new ProductionFacility { Code = "Facility2", Name = "Factory B" }
            };

            var dbSetMock = GetDbSetMock(facilities);
            repositoryMock.Setup(repo => repo.GetQueryableAsync<ProductionFacility>(cancellationToken))
                .ReturnsAsync(dbSetMock.Object);

            // Act
            var result = await repository.GetProductionFacilityByCodeAsync(code, cancellationToken);

            // Assert
            if (shouldExist)
            {
                Assert.IsNotNull(result);
                Assert.That(result!.Code, Is.EqualTo(code));
            }
            else
            {
                Assert.IsNull(result);
            }

            repositoryMock.Verify(repo => repo.GetQueryableAsync<ProductionFacility>(cancellationToken), Times.Once);
        }

        [Test]
        [TestCase(3, Description = "Finds all EquipmentPlacementContracts.")]
        [TestCase(0, Description = "Finds no EquipmentPlacementContracts when database is empty.")]
        public async Task GetEquipmentPlacementContractsAsync_TestCases(int contractCount)
        {
            // Arrange
            var contracts = Enumerable.Range(1, contractCount).Select(i => new EquipmentPlacementContract
            {
                Code = $"Contract{i}",
                ProductionFacilityCode = $"Facility{i}",
                ProcessEquipmentTypeCode = $"Type{i}",
                EquipmentQuantity = i * 10
            }).ToList();

            var dbSetMock = GetDbSetMock(contracts);
            repositoryMock.Setup(repo => repo.GetQueryableAsync<EquipmentPlacementContract>(cancellationToken))
                .ReturnsAsync(dbSetMock.Object);

            // Act
            var result = await repository.GetEquipmentPlacementContractsAsync(cancellationToken);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(contractCount));
            repositoryMock.Verify(repo => repo.GetQueryableAsync<EquipmentPlacementContract>(cancellationToken), Times.Once);
        }
    }
}