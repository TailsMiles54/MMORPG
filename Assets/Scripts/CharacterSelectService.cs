using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterSelectService : MonoBehaviour, IInitializable
{
    [Inject] private CloudSaveService _cloudSaveService;
    [Inject] private DiContainer _diContainer;

    [SerializeField] private CharacterSelectPanel _characterSelectPanel;
    [SerializeField] private CreateCharacterPanel _createCharacterPanel;

    private List<CloudSaveService.CharacterSaveData> _characters = new List<CloudSaveService.CharacterSaveData>();
    
    public async void GetCharacters()
    {
        _characters = await _cloudSaveService.LoadCharacters();

        if (_characters == null)
        {
            _characters = new List<CloudSaveService.CharacterSaveData>()
            {
                new CloudSaveService.CharacterSaveData(),
                new CloudSaveService.CharacterSaveData(),
                new CloudSaveService.CharacterSaveData(),
            };
            await _cloudSaveService.SaveCharacters(_characters);
        }
        
        _characterSelectPanel.Setup(_characters);
    }

    public void Initialize()
    {
        GetCharacters();
        _diContainer.Bind<CharacterSelectPanel>().FromInstance(_characterSelectPanel).NonLazy();
        _diContainer.Bind<CreateCharacterPanel>().FromInstance(_createCharacterPanel).NonLazy();
    }
}