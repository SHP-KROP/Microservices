using AuthServer.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<UserDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            if (userRegisterDTO.UserName == null)
            {
                userRegisterDTO.UserName = Guid.NewGuid().ToString();
            }

            var user = _mapper.Map<ApplicationUser>(userRegisterDTO);
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create user");
            }

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<string> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user == null)
            {
                throw new InvalidOperationException($"There is no user with email {userLoginDTO.Email}");
            }

            var result = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            if (!result)
            {
                throw new InvalidOperationException("Wrong password");
            }

            var jwtServece = new JwtService(_configuration);

            var tokenString = jwtServece.GenerateJwtToken(user.Id, user.UserName, user.Email);

            return tokenString;
        }
    }
}
