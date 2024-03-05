using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CharacterButtonController : MonoBehaviour
{
    [SerializeField] private CharacterButtonView _characterButtonView;

    public void Setup(CloudSaveService.CharacterSaveData characterSaveData)
    {
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
