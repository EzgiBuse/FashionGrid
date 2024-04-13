using FashionGrid.UI.Models.Dtos;
using FashionGrid.UI.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity.Data;

namespace FashionGrid.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenProvider _tokenProvider;

        public LoginController(IUserService userService, ITokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                LoginRequestDto loginRequest = new();
                return View(loginRequest);
            }
            catch (Exception e)
            {

                return View();
            }

        }


        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginRequestDto loginRequestDto)
        {

            try
            {
                ResponseDto responseDto = await _userService.LoginAsync(loginRequestDto);
                if (responseDto.IsSuccess)
                {
                    LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                    await SignInUser(loginResponse);
                    _tokenProvider.SetToken(loginResponse.Token);
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Welcome!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["NotificationType"] = "error";
                    TempData["NotificationMessage"] = "Email or password is incorrect";

                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                TempData["NotificationMessage"] = "Email or password is incorrect!";

                return RedirectToAction("Login");
            }


        }

        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(loginResponseDto.Token);
                if (jwt != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                        jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
                    identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                        jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
                    identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                        jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));

                    identity.AddClaim(new Claim(ClaimTypes.Name,
                       jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
                    //identity.AddClaim(new Claim(ClaimTypes.Role,
                    //   jwt.Claims.FirstOrDefault(x => x.Type == "role").Value));

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                }
            }
            catch (Exception e)
            {

               
            }

        }


        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                _tokenProvider.ClearToken();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

        }

    }
}

