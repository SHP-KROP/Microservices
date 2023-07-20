using AuthServer.DTO;
using AuthServer.Mapping;
using AuthServer.Services;
using AuthServer.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AuthServer.UnitTests
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly Mock<IMapper> _mockMapper;

        public AuthServiceTests()
        {
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>());

            _mockJwtService = new Mock<IJwtService>();
            _mockMapper = new Mock<IMapper>();

            _authService = new AuthService(_mockUserManager.Object, _mockMapper.Object, _mockJwtService.Object);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsUserDto()
        {
            var userRegisterDTO = new UserRegisterDTO
            {
                Email = "test@example.com",
                Password = "testPassword",
            };

            _mockUserManager.Setup(m => m.FindByEmailAsync(userRegisterDTO.Email))
                   .ReturnsAsync((ApplicationUser)null);

            var user = new ApplicationUser();
            _mockMapper.Setup(m => m.Map<ApplicationUser>(userRegisterDTO))
                       .Returns(user);

            var identityResult = IdentityResult.Success;
            _mockUserManager.Setup(m => m.CreateAsync(user, userRegisterDTO.Password))
                            .ReturnsAsync(identityResult);

            var userDTO = new UserDTO(); 
            _mockMapper.Setup(m => m.Map<UserDTO>(user))
                       .Returns(userDTO);

            var result = await _authService.Register(userRegisterDTO);

            Assert.NotNull(result);
            Assert.Equal(userDTO, result);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsJwtToken()
        {
            var userLoginDTO = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "testPassword",
            };

            var user = new ApplicationUser
            {
                Email = userLoginDTO.Email,
            };

            _mockUserManager.Setup(m => m.FindByEmailAsync(userLoginDTO.Email))
                .Returns(Task.FromResult(user));

            _mockUserManager.Setup(m => m.CheckPasswordAsync(user, userLoginDTO.Password))
                .Returns(Task.FromResult(true));

            var expectedToken = "generated_jwt_token";
            _mockJwtService.Setup(m => m.GenerateJwtToken(user)).Returns(expectedToken);

            var result = await _authService.Login(userLoginDTO);


            Assert.NotNull(result);
            Assert.Equal(expectedToken, result);
        }
    }
}