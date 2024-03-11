using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Friends;
using UnityEngine;
using Zenject;

public class FriendService : IInitializable
{
    public event Action Initialized; 
    public async void Initialize()
    {
        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        await FriendsService.Instance.InitializeAsync();

        var friends = FriendsService.Instance.Friends;
        
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
        Initialized?.Invoke();
    }
}