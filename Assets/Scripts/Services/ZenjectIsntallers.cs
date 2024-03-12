using System;
using UnityEngine;
using Zenject;

public class ZenjectInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<AuthService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<CloudSaveService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<PhotonService>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<FriendService>().AsCached().NonLazy();
        Container.Bind<CharacterService>().AsCached().NonLazy();
    }
}

public class PhotonService : IInitializable
{
    public event Action Initialized;

    public void Initialize()
    {
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
        Initialized?.Invoke();
    }
}

public class CharacterService
{
    public string CurrentCharacter { get; private set; }
    
    public void SetCharacterId(string id)
    {
        CurrentCharacter = id;
    }
}