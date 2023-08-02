using AuthServer.DTO;
using AuthServer.Exceptions;
using AuthServer.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services
{
    public class AuthService
    {
        private readonly IUserManagerDecorator _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public AuthService(IUserManagerDecorator userManager, IMapper mapper, IJwtService jwtService)
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
                throw new AlreadyExistingUserException(existingUser.Email);
            }

            var user = _mapper.Map<ApplicationUser>(userRegisterDTO);
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);

            if (!result.Succeeded)
            {
                throw new CreatingUserException(result.Errors);
            }

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<string> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user == null)
            {
                throw new NotExistingUserException(userLoginDTO.Email);
            }

            var passwordMatches = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            if (!passwordMatches)
            {
                throw new WrongPasswordException();
            }

            var tokenString = _jwtService.GenerateJwtToken(user);

            return tokenString;
        }
    }
}
