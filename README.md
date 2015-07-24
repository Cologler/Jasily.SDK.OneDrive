# Jasily.SDK.OneDrive

> a perfect C# lib for OneDrive, better than official.

## auth

two way to auth, code flow or token flow.

#### code flow

``` cs
// 1. choose you permissions what you need.
var Permissions =
    AuthenticationPermissions.WindowsLiveSignIn |
    AuthenticationPermissions.WindowsLiveOfflineAccess |
    AuthenticationPermissions.OnedriveReadonly;

// 2. get auth uri
var authenticator = new CodeFlowAuthenticator(your ClientId, your ClientSecret);
var uri Authenticator.BuildAuthenticateUri(Permissions); // use this uri for auth

// 3. handle WebBrowser.Navigating
if (e.Uri != null && Authenticator.IsRedirectUriVaild(e.Uri.ToString()))
{
    authenticationInfo = await Authenticator.TryAuthenticateWithRedirectUri(e.Uri.ToString());
    // authenticationInfo maybe null if redeem token
}
```

you can found access token in authenticationInfo.

## get folder or file

``` cs
// 1. get drive
var controller = new OneDriveWebController(your access token);
var result = await controller.GetPrimaryDriveAsync();
if (result.IsSuccess)
{
    your drive = result.Result;
}

// 2. get root folder
var result = await drive.GetRootAsync();
if (result.IsSuccess)
{
    your root = result.Result;
}

// 3. get folders
var result = await root.ListChildrenAsync();
if (result.IsSuccess)
{
    your folder and files = result.Result;
}
```

the request was wraped, you don't need to catch WebException, only catch other Exception.

but any Exception was bug for this sdk, a better sdk will never let app crash.

^_^
