namespace Jasily.SDK.OneDrive.Authentications
{
    public interface IAuthenticationInfo
    {
        string TokenType { get; }
        int ExpiresIn { get; }
        string Scope { get; }
        string AccessToken { get; }
    }
}