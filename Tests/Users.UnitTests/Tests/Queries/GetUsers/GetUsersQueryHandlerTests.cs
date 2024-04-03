using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Tests.Fixtures;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Moq;
using System.Linq.Expressions;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Handlers.Queries.GetUsers;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Queries.GetUsers
{
    public class GetUsersQueryHandlerTests :RequestHandlerTestBase<GetUsersQuery, BaseListDto<GetUserDto>>
    {
        private readonly Mock<IBaseReadRepository<ApplicationUser>> _usersMok = new();

        private readonly Mock<ICurrentUserService> _currentServiceMok = new();

        private readonly Mock<ApplicationUsersListMemoryCache> _mockUsersMemoryCache = new();

        private readonly IMapper _mapper;

        public GetUsersQueryHandlerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetUsersQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetUsersQuery, BaseListDto<GetUserDto>> CommandHandler =>
            new GetUsersQueryHandler(_usersMok.Object, _mapper, _mockUsersMemoryCache.Object);
        [Fact]
        public async Task Should_BeValid_When_GetUsersByAdmin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetUsersQuery();

            var users = TestFixture.Build<ApplicationUser>().CreateMany(10).ToArray();
            var count = users.Length;

            _currentServiceMok.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(true);

            _usersMok.Setup(
                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)
            ).ReturnsAsync(users);

            _usersMok.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_BeValid_When_GetUsersByClient()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetUsersQuery();

            var users = TestFixture.Build<ApplicationUser>().CreateMany(10).ToArray();
            var count = users.Length;

            _currentServiceMok.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(false);

            _usersMok.Setup(
                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)
            ).ReturnsAsync(users);

            _usersMok.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}
