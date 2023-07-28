using AuthServer.DTO;
using AuthServer.Mapping;
using AuthServer.Services;
using AuthServer.Services.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AuthServer.UnitTests
{
    public class AuthServiceTests
    {
        private const string Email = "test@example.com";
        private const string Password = "testPassword";

        private readonly AuthService _authService;
        private readonly Mock<IUserManagerDecorator> _mockUserManager;
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly IMapper _mapper;
        public AuthServiceTests()
        {
            _mockUserManager = new Mock<IUserManagerDecorator>();

            _mockJwtService = new Mock<IJwtService>();
            _mapper = new Mapper(
                new MapperConfiguration(
                    x => x.AddProfile(new AuthMappingProfile())
                    )
                );

            _authService = new AuthService(_mockUserManager.Object, _mapper, _mockJwtService.Object);
        }

        [Fact]
        public async Task ItShouldReturnUserDtoForRegisteredUser_WhenRegisteredSuccessfully()
        {
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = Email,
                Password = Password,
            };
            
            var expectedUser = new ApplicationUser
            {
                Email = Email
            };

            _mockUserManager
                .Setup(m => m.FindByEmailAsync(Email))
                .ReturnsAsync(null as ApplicationUser);

            _mockUserManager
                .Setup(m => m.CreateAsync(
                    It.Is<ApplicationUser>(x => x.Email == Email), Password))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _authService.Register(userRegisterDTO);

            result.Should().BeOfType<UserDTO>();
            result.Email.Should().Be(expectedUser.Email);
        }

        [Fact]
        public async Task ItShouldReturnJwtToken_WhenUserSuccessfullyLoggedIn()
        {
            var userLoginDTO = new UserLoginDTO
            {
                Email = Email,
                Password = Password,
            };

            var expectedUser = new ApplicationUser { Email = Email,};

            _mockUserManager
                .Setup(m => m.FindByEmailAsync(Email))
                .ReturnsAsync(expectedUser);

            _mockUserManager
                .Setup(m => m.CheckPasswordAsync(expectedUser, Password))
                .ReturnsAsync(true);

            const string expectedToken = "generated_jwt_token";
            _mockJwtService
                .Setup(m => m.GenerateJwtToken(expectedUser))
                .Returns(expectedToken);

            var result = await _authService.Login(userLoginDTO);

            result.Should().Be(expectedToken);
        }
    }
}