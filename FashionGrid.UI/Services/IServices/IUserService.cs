using FashionGrid.UI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.UI.Services.IServices
{
    public interface IUserService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
