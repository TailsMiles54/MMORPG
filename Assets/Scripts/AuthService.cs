using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

public class AuthService : IInitializable
{
    [Inject] private SceneService _sceneService;
    public event Action Initialized; 
    
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
        Initialized?.Invoke();
    }
    
    private void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            var accessToken = PlayerAccountService.Instance.AccessToken;
            Debug.Log($"Player:{accessToken} signed in.");
        };

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
    
    public async Task RegisterWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
            _sceneService.SelectCharacterScene();
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
    
    public async Task LoginWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
            _sceneService.SelectCharacterScene();
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