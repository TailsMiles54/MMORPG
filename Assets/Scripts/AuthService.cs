using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

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