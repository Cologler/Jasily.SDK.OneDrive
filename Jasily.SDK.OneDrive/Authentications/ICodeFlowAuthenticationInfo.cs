namespace Jasily.SDK.OneDrive.Authentications
{
    public interface ICodeFlowAuthenticationInfo : IAuthenticationInfo
    {
        string RefreshToken { get; }
    }
}