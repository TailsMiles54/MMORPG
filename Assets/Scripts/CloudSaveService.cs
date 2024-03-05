using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CloudSaveService : IInitializable
{
    [Inject] private SceneService _sceneService;
    public event Action Initialized;

    public void Initialize()
    {
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
        Initialized?.Invoke();
        SaveData();
    }

    public async void SaveData()
    {
        var playerData = new Dictionary<string, object>
        {
            {
                "characters", new List<CharacterSaveData>()
                {
                    new CharacterSaveData(),
                    new CharacterSaveData(),
                    new CharacterSaveData()
                }
            }
        };
        var result = await Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }

    public async Task<List<CharacterSaveData>> LoadCharacters()
    {
        var test = await Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>
        {
            "characters"
        });


        if (test.TryGetValue("characters", out var firstKey))
        {
            return firstKey.Value.GetAs<List<CharacterSaveData>>();
        }

        return null;
    }

    public void LoadDataAfterAuth()
    {
        // var characters = LoadCharacters().Result;
        //
        // if (characters == null || characters.IsEmpty())
        // {
        //     SaveData();
        //     characters = LoadCharacters().Result;
        // }

        _sceneService.SelectCharacterScene();
    }

    public class CharacterSaveData
    {
        public string Nickname;
        public int Level;
    }
}