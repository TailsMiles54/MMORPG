using System;
using System.Linq;
using System.Text;
using UnityEngine;
using WebSocketSharp;
using Zenject;

public class CharacterButtonController : MonoBehaviour
{
    [SerializeField] private CharacterButtonView _characterButtonView;
    private CharacterAppearanceController _characterAppearanceController;
    private GameObject _characterView;

    [Inject] private CharacterEditor _characterEditor;
    [Inject] private CharacterSelectPanel _characterSelectPanel;
    [Inject] private CharacterSelectService _characterSelectService;
    [Inject] private CloudSaveService _cloudSaveService;

    public void Setup(CloudSaveService.CharacterSaveData characterSaveData, CharacterAppearanceController characterAppearanceController, GameObject characterView)
    {
        _characterSelectPanel.DeleteButton.onClick.RemoveAllListeners();
        _characterButtonView.Button.onClick.RemoveAllListeners();
        _characterSelectPanel.DeleteButton.gameObject.SetActive(false);
        
        _characterSelectPanel.DeleteButton.onClick.AddListener(DeleteCharacter);
            
        _characterAppearanceController = characterAppearanceController;
        _characterView = characterView;
        
        _characterButtonView.Button.onClick.RemoveAllListeners();
        
        if (characterSaveData.CharacterId.IsNullOrEmpty())
        {
            _characterButtonView.Text.text = "Create new Character";
            
            _characterButtonView.Button.onClick.AddListener(CreateCharacter);
            return;
        }
        
        var sb = new StringBuilder();
        sb.Append($"{characterSaveData.Nickname}\n \n Level: {characterSaveData.Level}");
        _characterButtonView.Text.text = sb.ToString();
        
        _characterButtonView.Button.onClick.AddListener(() => ShowCharacter(characterSaveData));
    }
    
    private void CreateCharacter()
    {
        _characterSelectPanel.DeleteButton.gameObject.SetActive(false);
        _characterSelectPanel.SelectedCharacter = String.Empty;
        _characterView.SetActive(true);
        _characterEditor.ClearEditor();
        _characterEditor.gameObject.SetActive(true);
        _characterSelectPanel.gameObject.SetActive(false);
    }

    private async void DeleteCharacter()
    {
        if (_characterSelectService.Characters.Any(x => x.CharacterId == _characterSelectPanel.SelectedCharacter))
        {
            var characterSaveData = _characterSelectService.Characters.First(x => x.CharacterId == _characterSelectPanel.SelectedCharacter);
            _characterSelectService.Characters.Remove(characterSaveData);
            _characterSelectService.Characters.Add(new CloudSaveService.CharacterSaveData());
        }
        
        await _cloudSaveService.SaveCharacterData(_characterSelectService.Characters);
        _characterSelectPanel.UpdateButtons(_characterSelectService.Characters);
    }

    private void ShowCharacter(CloudSaveService.CharacterSaveData characterSaveData)
    {
        _characterSelectPanel.SelectedCharacter = characterSaveData.CharacterId;
        _characterSelectPanel.DeleteButton.gameObject.SetActive(true);
        _characterView.SetActive(true);
        _characterAppearanceController.SetupFromCharacterData(characterSaveData.Gender, characterSaveData.AppearanceSaveData);
    }
}
