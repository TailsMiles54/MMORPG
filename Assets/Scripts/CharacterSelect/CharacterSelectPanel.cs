using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Zenject;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private CharacterButtonController _characterButtonController;
    [SerializeField] private Transform _parentForButtons;
    [SerializeField] private CharacterAppearanceController _characterAppearanceController;
    [SerializeField] private GameObject _characterView;
    [field :SerializeField] public Button DeleteButton { get; private set; }
    
    private List<CharacterButtonController> _characterButtonControllers = new List<CharacterButtonController>();
    [Inject] private CharacterSelectService _characterSelectService;
    [Inject] private AuthService _authService;
    [Inject] private DiContainer _diContainer;
    [Inject] private CharacterService _characterService;
    [Inject] private SceneService _sceneService;

    [HideInInspector] public string SelectedCharacter;
    
    public void Setup(List<CloudSaveService.CharacterSaveData> characters)
    {
        foreach (var characterSaveData in characters)
        {
            var newCharacterButtonObject = _diContainer.InstantiatePrefab(_characterButtonController, _parentForButtons);
            var newCharacterButtonController = newCharacterButtonObject.GetComponent<CharacterButtonController>();
            
            _characterButtonControllers.Add(newCharacterButtonController);
            
            newCharacterButtonController.Setup(characterSaveData, _characterAppearanceController, _characterView);
        }
    }

    public void Exit()
    {
        _authService.LogOut(); 
    }

    public void UpdateButtons(List<CloudSaveService.CharacterSaveData> characters)
    {
        _characterView.SetActive(false);
        foreach (var characterButtonController in _characterButtonControllers)
        {
            var index = _characterButtonControllers.IndexOf(characterButtonController);
            var character = characters[index];
            
            characterButtonController.Setup(character, _characterAppearanceController, _characterView);
        }
    }

    public void Start()
    {
        if(SelectedCharacter.IsNullOrEmpty())
            return;
        
        _characterService.SetCharacterId(SelectedCharacter);
        _sceneService.GameTestScene();
    }
}