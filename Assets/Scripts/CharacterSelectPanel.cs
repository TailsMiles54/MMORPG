using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private List<CharacterButtonController> _characterButtonControllers;
    
    public void Setup(List<CloudSaveService.CharacterSaveData> characters)
    {
        foreach (var characterSaveData in characters)
        {
            var index = characters.IndexOf(characterSaveData);
            _characterButtonControllers[index].Setup(characterSaveData);
        }
    }
}
