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