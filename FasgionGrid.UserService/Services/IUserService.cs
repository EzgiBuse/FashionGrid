using FasgionGrid.UserService.Models.Dtos;

namespace FasgionGrid.UserService.Services
{
    public interface IUserService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<string> RegisterDealer(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
