using UnityEngine;
using Zenject;

public class AuthSceneInstallers : MonoInstaller
{
    [SerializeField] private AuthPanelController _authPanelController;
    [SerializeField] private Transform _canvasTransform;
    
    public override void InstallBindings()
    {
        Container.Bind<AuthPanelController>().FromInstance(_authPanelController);
    }
}

