using System.Collections;
using System.Collections.Generic;
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

    public void Setup(CloudSaveService.CharacterSaveData characterSaveData, CharacterAppearanceController characterAppearanceController, GameObject characterView)
    {
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
        _characterView.SetActive(true);
        _characterEditor.ClearEditor();
        _characterEditor.gameObject.SetActive(true);
        _characterSelectPanel.gameObject.SetActive(false);
    }

    private void ShowCharacter(CloudSaveService.CharacterSaveData characterSaveData)
    {
        _characterView.SetActive(true);

        _characterAppearanceController.SetupFromCharacterData(characterSaveData.Gender, characterSaveData.AppearanceSaveData);
    }
}
