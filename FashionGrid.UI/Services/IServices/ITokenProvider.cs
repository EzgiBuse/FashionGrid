namespace FashionGrid.UI.Services.IServices
{
    public interface ITokenProvider
    {
        void SetToken(string Token);
        string? GetToken();
        void ClearToken();
    }
}
