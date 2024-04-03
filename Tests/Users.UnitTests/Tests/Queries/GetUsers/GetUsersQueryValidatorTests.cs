using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Users.Application.Handlers.Queries.GetUsers;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Queries.GetUsers
{
    public class GetUsersQueryValidatorTests:ValidatorTestBase<GetUsersQuery>
    {
        public GetUsersQueryValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetUsersQuery> TestValidator =>
            TestFixture.Create<GetUsersQueryValidator>();

        [Fact]
        public void Should_BeValid_When_RequestIsValid()
        {
            // arrange
            var query = new GetUsersQuery
            {
                Limit = 5,
                Offset = 10,
                FreeText = "123",
                
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(20)]
        public void Should_NotValid_When_LimitIsValid(int limit)
        {
            // arrange
            var query = new GetUsersQuery
            {
                Limit = limit,
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        [FixtureInlineAutoData(21)]
        public void Should_NotBeValid_When_IncorrectLimit(int limit)
        {
            // arrange
            var query = new GetUsersQuery
            {
                Limit = limit,
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        public void Should_NotBeValid_When_IncorrectOffset(int offset)
        {
            // arrange
            var query = new GetUsersQuery
            {
                Offset = offset,
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("123")]
        [FixtureInlineAutoData("1#*&^%$#@#$%±~`}{][\\|?/.,<>")]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890")]
        public void Should_BeValid_When_FreeTextIsValid(string freeText)
        {
            // arrange
            var query = new GetUsersQuery
            {
                FreeText = freeText,
            };

            // act & assert
            AssertValid(query);
        }

        [Fact]
        public void Should_NotBeValid_When_FreeTextIsGreatThen50()
        {
            // arrange
            var query = new GetUsersQuery
            {
                FreeText = new string('s',51)
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
