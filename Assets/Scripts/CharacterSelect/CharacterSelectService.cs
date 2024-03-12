using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CharacterSelectService : MonoBehaviour, IInitializable
{
    [Inject] private CloudSaveService _cloudSaveService;
    [Inject] private DiContainer _diContainer;

    [SerializeField] private CharacterSelectPanel _characterSelectPanel;
    [SerializeField] private CharacterEditor _characterEditor;

    public List<CloudSaveService.CharacterSaveData> Characters { get; private set; } = new List<CloudSaveService.CharacterSaveData>();
    
    private async void GetCharacters()
    {
        Characters = await _cloudSaveService.LoadCharacters();

        if (Characters == null)
        {
            Characters = new List<CloudSaveService.CharacterSaveData>()
            {
                new CloudSaveService.CharacterSaveData(),
                new CloudSaveService.CharacterSaveData(),
                new CloudSaveService.CharacterSaveData(),
            };
            await _cloudSaveService.SaveCharacters(Characters);
        }
        
        _characterSelectPanel.Setup(Characters);
    }

    public async Task OpenCharactersList()
    {
        var loadedCharacters = await _cloudSaveService.LoadCharacters();
        _characterSelectPanel.UpdateButtons(loadedCharacters);
        
        _characterSelectPanel.gameObject.SetActive(true);
        _characterEditor.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        GetCharacters();
        _diContainer.Bind<CharacterSelectPanel>().FromInstance(_characterSelectPanel).NonLazy();
        _diContainer.Bind<CharacterEditor>().FromInstance(_characterEditor).NonLazy();
    }
}