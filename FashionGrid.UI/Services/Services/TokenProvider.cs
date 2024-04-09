using FashionGrid.UI.Services.IServices;

namespace FashionGrid.UI.Services.Services
{
    public class TokenProvider :ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete("JwtToken");
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue("JwtToken", out token);
            if (hasToken == true)
            {
                return token;
            }
            else
            {
                return null;
            }
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append("JwtToken", token);
        }
    }
}
