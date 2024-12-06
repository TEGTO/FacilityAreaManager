using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;

namespace FacilityAreaManagerApi.Tests
{
    [TestFixture]
    internal class AutoMapperProfileTests
    {
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            mapper = configuration.CreateMapper();
        }

        [Test]
        public void Mapping_AddEquipmentPlacementContractRequest_To_EquipmentPlacementContract()
        {
            // Arrange
            var request = new AddEquipmentPlacementContractRequest
            {
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 10
            };

            // Act
            var result = mapper.Map<EquipmentPlacementContract>(request);

            // Assert
            Assert.That(result.ProductionFacilityCode, Is.EqualTo(request.ProductionFacilityCode));
            Assert.That(result.ProcessEquipmentTypeCode, Is.EqualTo(request.ProcessEquipmentTypeCode));
            Assert.That(result.EquipmentQuantity, Is.EqualTo(request.EquipmentQuantity));
        }

        [Test]
        public void Mapping_AddProcessEquipmentTypeRequest_To_ProcessEquipmentType()
        {
            // Arrange
            var request = new AddProcessEquipmentTypeRequest
            {
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            // Act
            var result = mapper.Map<ProcessEquipmentType>(request);

            // Assert
            Assert.That(result.Name, Is.EqualTo(request.Name));
            Assert.That(result.Area, Is.EqualTo(request.Area));
        }

        [Test]
        public void Mapping_AddProductionFacilityRequest_To_ProductionFacility()
        {
            // Arrange
            var request = new AddProductionFacilityRequest
            {
                Name = "FacilityA",
                StandardAreaForEquipment = 1000.0f
            };

            // Act
            var result = mapper.Map<ProductionFacility>(request);

            // Assert
            Assert.That(result.Name, Is.EqualTo(request.Name));
            Assert.That(result.StandardAreaForEquipment, Is.EqualTo(request.StandardAreaForEquipment));
        }

        [Test]
        public void Mapping_EquipmentPlacementContract_To_EquipmentPlacementContractResponse()
        {
            // Arrange
            var contract = new EquipmentPlacementContract
            {
                Code = "Contract1",
                ProductionFacilityCode = "Facility1",
                ProcessEquipmentTypeCode = "Type1",
                EquipmentQuantity = 10
            };

            // Act
            var result = mapper.Map<EquipmentPlacementContractResponse>(contract);

            // Assert
            Assert.That(result.Code, Is.EqualTo(contract.Code));
            Assert.That(result.ProductionFacilityCode, Is.EqualTo(contract.ProductionFacilityCode));
            Assert.That(result.ProcessEquipmentTypeCode, Is.EqualTo(contract.ProcessEquipmentTypeCode));
            Assert.That(result.EquipmentQuantity, Is.EqualTo(contract.EquipmentQuantity));
        }

        [Test]
        public void Mapping_ProcessEquipmentType_To_ProcessEquipmentTypeResponse()
        {
            // Arrange
            var equipmentType = new ProcessEquipmentType
            {
                Code = "Type1",
                Name = "EquipmentTypeA",
                Area = 50.0f
            };

            // Act
            var result = mapper.Map<ProcessEquipmentTypeResponse>(equipmentType);

            // Assert
            Assert.That(result.Code, Is.EqualTo(equipmentType.Code));
            Assert.That(result.Name, Is.EqualTo(equipmentType.Name));
            Assert.That(result.Area, Is.EqualTo(equipmentType.Area));
        }

        [Test]
        public void Mapping_ProductionFacility_To_ProductionFacilityResponse()
        {
            // Arrange
            var facility = new ProductionFacility
            {
                Code = "Facility1",
                Name = "FacilityA",
                StandardAreaForEquipment = 1000.0f
            };

            // Act
            var result = mapper.Map<ProductionFacilityResponse>(facility);

            // Assert
            Assert.That(result.Code, Is.EqualTo(facility.Code));
            Assert.That(result.Name, Is.EqualTo(facility.Name));
            Assert.That(result.StandardAreaForEquipment, Is.EqualTo(facility.StandardAreaForEquipment));
        }
    }
}