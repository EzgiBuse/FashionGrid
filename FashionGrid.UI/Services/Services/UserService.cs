using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using static FashionGrid.UI.Utilities.Standard;

namespace FashionGrid.UI.Services.Services
{
    public class UserService :IUserService
    {
        private readonly IBaseService _baseService;

        public UserService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = UserAPIBase + "/api/Users/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = loginRequestDto,
                Url = UserAPIBase + "/api/authAPI/Login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = UserAPIBase + "/api/authAPI/Register"
            }, withBearer: false);
        }
    }
}

