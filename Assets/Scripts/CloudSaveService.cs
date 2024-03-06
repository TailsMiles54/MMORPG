using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModestTree;
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
        var characters = LoadCharacters().Result;
        
        if (characters == null || characters.IsEmpty())
        {
            SaveData();
            characters = LoadCharacters().Result;
        }
    }

    public class CharacterSaveData
    {
        public string CharacterId;
        public string Nickname;
        public int Level;
        public ClassType ClassType;
    }

    public enum ClassType
    {
        TheAdventurer = 0,
        Warrior = 1,
        Archer = 2,
        Mage = 3,
    }

    public async Task SaveCharacters(List<CharacterSaveData> characters)
    {
        var playerData = new Dictionary<string, object>
        {
            {
                "characters", characters
            }
        };
        
        await Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }
}