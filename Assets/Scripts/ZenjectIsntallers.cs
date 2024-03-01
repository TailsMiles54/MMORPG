using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ZenjectInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AuthService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<CloudSaveService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<PhotonService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<FriendService>().AsCached().NonLazy();
    }
}

public class AuthService : IInitializable
{
    public async void Initialize()
    {
        try
        {
            await UnityServices.InitializeAsync();
            SetupEvents();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        await InitSignIn();
        
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    }
    
    private void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += SignedIn;

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

    private async void SignedIn()
    {
        var accessToken = PlayerAccountService.Instance.AccessToken;
        await SignInWithUnityAsync(accessToken);
    }

    private async Task InitSignIn()
    { 
        await PlayerAccountService.Instance.StartSignInAsync();
    }
    
    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}

public class CloudSaveService : IInitializable
{
    public void Initialize()
    {
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    } 
}

public class PhotonService : IInitializable
{
    public void Initialize()
    {
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    }
}

public class FriendService : IInitializable
{
    public void Initialize()
    {
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    }
}
