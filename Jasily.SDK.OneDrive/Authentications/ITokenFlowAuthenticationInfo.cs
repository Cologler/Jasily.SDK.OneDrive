namespace Jasily.SDK.OneDrive.Authentications
{
    public interface ITokenFlowAuthenticationInfo : IAuthenticationInfo
    {
        string AuthenticationToken { get; }
        string UserId { get; }
    }
}