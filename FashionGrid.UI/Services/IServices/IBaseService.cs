using FashionGrid.UI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.UI.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
