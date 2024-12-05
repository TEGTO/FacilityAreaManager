using FacilityAreaManagerApi.Infrastructure.Dtos;

namespace FacilityAreaManagerApi.Infrastructure.Validators.Tests
{
    [TestFixture]
    internal class AddProductionFacilityRequestValidatorTests
    {
        private AddProcessEquipmentTypeRequestValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new AddProcessEquipmentTypeRequestValidator();
        }

        private static IEnumerable<TestCaseData> ValidationTestCases()
        {
            yield return new TestCaseData(
                new AddProcessEquipmentTypeRequest
                {
                    Name = "ValidEquipment",
                    Area = 100.5f
                },
                true
            ).SetDescription("Valid request with all fields correctly populated.");

            yield return new TestCaseData(
                new AddProcessEquipmentTypeRequest
                {
                    Name = "",
                    Area = 100.5f
                },
                false
            ).SetDescription("Invalid request: Empty Name.");

            yield return new TestCaseData(
                new AddProcessEquipmentTypeRequest
                {
                    Name = null!,
                    Area = 100.5f
                },
                false
            ).SetDescription("Invalid request: Null Name.");

            yield return new TestCaseData(
                new AddProcessEquipmentTypeRequest
                {
                    Name = "ValidEquipment",
                    Area = 0
                },
                false
            ).SetDescription("Invalid request: Area is 0.");

            yield return new TestCaseData(
                new AddProcessEquipmentTypeRequest
                {
                    Name = "ValidEquipment",
                    Area = -10
                },
                false
            ).SetDescription("Invalid request: Negative Area.");

            yield return new TestCaseData(
                new AddProcessEquipmentTypeRequest
                {
                    Name = "",
                    Area = -5
                },
                false
            ).SetDescription("Invalid request: Empty Name and Negative Area.");
        }

        [Test]
        [TestCaseSource(nameof(ValidationTestCases))]
        public void ValidateRequest_TestCases(AddProcessEquipmentTypeRequest request, bool isValid)
        {
            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.EqualTo(isValid));
        }
    }
}