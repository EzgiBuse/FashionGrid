using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FashionGrid.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                ResponseDto result = await _userService.RegisterAsync(registrationRequestDto);

                if (result.IsSuccess)
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Registration successful";
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    TempData["NotificationType"] = "error";
                    TempData["NotificationMessage"] = "Registration failed";
                    return RedirectToAction("Register");
                }
            }
            catch (Exception ex)
            {

                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "An error occurred during registration. Please try again later.";
                return RedirectToAction("Register");
            }
        }
    }
}
