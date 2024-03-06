using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private List<CharacterButtonController> _characterButtonControllers;
    [Inject] private CharacterSelectService _characterSelectService;
    [Inject] private DiContainer _diContainer;
    
    public void Setup(List<CloudSaveService.CharacterSaveData> characters)
    {
        foreach (var characterSaveData in characters)
        {
            var index = characters.IndexOf(characterSaveData);
            _characterButtonControllers[index].Setup(characterSaveData);
            
            _diContainer.Bind<CharacterButtonController>().FromInstance(_characterButtonControllers[index]).NonLazy();
        }
    }
}
