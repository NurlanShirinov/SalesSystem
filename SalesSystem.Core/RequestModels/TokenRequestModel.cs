namespace SalesSystem.Core.RequestModels
{
    public class TokenRequestModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
