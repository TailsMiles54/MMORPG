using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private CharacterButtonController _characterButtonController;
    [SerializeField] private Transform _parentForButtons;
    
    private List<CharacterButtonController> _characterButtonControllers = new List<CharacterButtonController>();
    [Inject] private CharacterSelectService _characterSelectService;
    [Inject] private DiContainer _diContainer;
    
    public void Setup(List<CloudSaveService.CharacterSaveData> characters)
    {
        foreach (var characterSaveData in characters)
        {
            var newCharacterButtonObject = _diContainer.InstantiatePrefab(_characterButtonController, _parentForButtons);
            var newCharacterButtonController = newCharacterButtonObject.GetComponent<CharacterButtonController>();
            
            _characterButtonControllers.Add(newCharacterButtonController);
            
            newCharacterButtonController.Setup(characterSaveData);
        }
    }

    public void UpdateButtons(List<CloudSaveService.CharacterSaveData> characters)
    {
        foreach (var characterButtonController in _characterButtonControllers)
        {
            var index = _characterButtonControllers.IndexOf(characterButtonController);
            var character = characters[index];
            
            characterButtonController.Setup(character);
        }
    }
}