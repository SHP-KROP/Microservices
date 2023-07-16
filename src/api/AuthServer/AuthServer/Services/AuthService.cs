using AuthServer.DTO;
using AuthServer.Exceptions;
using AuthServer.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<UserDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            if (existingUser != null)
            {
                throw new AlreadyExistingUserException("User with this email already exists");
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
                throw new NotExistingUserException($"There is no user with email {userLoginDTO.Email}");
            }

            var result = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            if (!result)
            {
                throw new InvalidOperationException("Wrong password");
            }

            var tokenString = _jwtService.GenerateJwtToken(user);

            return tokenString;
        }
    }
}
