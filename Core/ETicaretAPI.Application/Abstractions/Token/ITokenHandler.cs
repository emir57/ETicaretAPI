namespace ETicaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken();
        string CreateRefreshToken();
    }
}
