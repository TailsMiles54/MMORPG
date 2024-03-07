using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private CharacterButtonController _characterButtonController;
    [SerializeField] private Transform _parentForButtons;
    
    private List<CharacterButtonController> _characterButtonControllers;
    [Inject] private CharacterSelectService _characterSelectService;
    [Inject] private DiContainer _diContainer;
    
    public void Setup(List<CloudSaveService.CharacterSaveData> characters)
    {
        foreach (var characterSaveData in characters)
        {
            var newCharacterButtonObject = _diContainer.InstantiatePrefab(_characterButtonController, _parentForButtons);
            var newCharacterButtonController = newCharacterButtonObject.GetComponent<CharacterButtonController>();
            newCharacterButtonController.Setup(characterSaveData);
        }
    }
}

// public class CharacterButtonControllerFactory : IFactory<CharacterButtonControllerFactory>
// {
//     public CharacterButtonControllerFactory Create()
//     {
//         var _characterButtonController = ;
//         
//         return var 
//     }
// }
