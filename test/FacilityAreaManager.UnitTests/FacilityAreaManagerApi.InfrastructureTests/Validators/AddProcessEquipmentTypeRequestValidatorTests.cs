using FacilityAreaManagerApi.Infrastructure.Dtos;
using FluentValidation.TestHelper;

namespace FacilityAreaManagerApi.Infrastructure.Validators.Tests
{
    [TestFixture]
    internal class AddProcessEquipmentTypeRequestValidatorTests
    {
        private AddEquipmentPlacementContractRequestValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new AddEquipmentPlacementContractRequestValidator();
        }

        private static IEnumerable<TestCaseData> ValidationTestCases()
        {
            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = "Type1",
                    ProductionFacilityCode = "Facility1",
                    EquipmentQuantity = 10
                },
                true
            ).SetDescription("Valid request with all fields provided correctly.");

            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = "",
                    ProductionFacilityCode = "Facility1",
                    EquipmentQuantity = 10
                },
                false
            ).SetDescription("Invalid request: Empty ProcessEquipmentTypeCode.");

            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = null!,
                    ProductionFacilityCode = "Facility1",
                    EquipmentQuantity = 10
                },
                false
            ).SetDescription("Invalid request: Null ProcessEquipmentTypeCode.");

            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = "Type1",
                    ProductionFacilityCode = "",
                    EquipmentQuantity = 10
                },
                false
            ).SetDescription("Invalid request: Empty ProductionFacilityCode.");

            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = "Type1",
                    ProductionFacilityCode = null!,
                    EquipmentQuantity = 10
                },
                false
            ).SetDescription("Invalid request: Null ProductionFacilityCode.");

            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = "Type1",
                    ProductionFacilityCode = "Facility1",
                    EquipmentQuantity = 0
                },
                false
            ).SetDescription("Invalid request: EquipmentQuantity is 0.");

            yield return new TestCaseData(
                new AddEquipmentPlacementContractRequest
                {
                    ProcessEquipmentTypeCode = "Type1",
                    ProductionFacilityCode = "Facility1",
                    EquipmentQuantity = -5
                },
                false
            ).SetDescription("Invalid request: Negative EquipmentQuantity.");
        }

        [Test]
        [TestCaseSource(nameof(ValidationTestCases))]
        public void ValidateRequest_TestCases(AddEquipmentPlacementContractRequest request, bool isValid)
        {
            // Act
            var result = validator.TestValidate(request);

            // Assert
            Assert.That(result.IsValid, Is.EqualTo(isValid));
        }
    }
}