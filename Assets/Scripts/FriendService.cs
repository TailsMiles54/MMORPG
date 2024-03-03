using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Friends;
using UnityEngine;

public class FriendService : Singleton<FriendService>
{
    public event Action Initialized; 
    public async void Initialize()
    {
        // Initialize all of the used services
        await UnityServices.InitializeAsync();

        // Sign in anonymously or using a provider
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        // Initialize friends service
        await FriendsService.Instance.InitializeAsync();

        // Start using the Friends SDK functionalities.
        var friends = FriendsService.Instance.Friends;
        Initialized?.Invoke();        
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    }
}