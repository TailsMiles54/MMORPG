using System;
using UnityEngine;
using Zenject;

public class CharacterSelectInstallers : MonoInstaller
{
    [SerializeField] private CharacterSelectService _characterSelectService;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CharacterSelectService>().FromInstance(_characterSelectService).AsCached().NonLazy();
    }
}