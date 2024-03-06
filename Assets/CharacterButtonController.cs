using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WebSocketSharp;

public class CharacterButtonController : MonoBehaviour
{
    [SerializeField] private CharacterButtonView _characterButtonView;

    public void Setup(CloudSaveService.CharacterSaveData characterSaveData)
    {
        if (characterSaveData.CharacterId.IsNullOrEmpty())
        {
            _characterButtonView.Text.text = "Create new Character";
            return;
        }
        
        var sb = new StringBuilder();

        sb.Append($"{characterSaveData.Nickname}\n \n Level: {characterSaveData.Level}");

        _characterButtonView.Text.text = sb.ToString();
    }
    
    public void CreateCharacter()
    {
        
    }

    public void SelectCharacter()
    {
        
    }
}
