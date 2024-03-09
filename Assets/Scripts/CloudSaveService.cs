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

    public async void Initialize()
    {
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
        Initialized?.Invoke();
    }

    public async Task SaveCharacterData(List<CharacterSaveData> characterSaveData)
    {
        var playerData = new Dictionary<string, object>{
            {"characters", characterSaveData},
        };
        await Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }

    public async Task<List<CharacterSaveData>> LoadCharacters()
    {
        try
        {
            var characters = await Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>
            {
                "characters"
            });
            
            
            if (characters.TryGetValue("characters", out var firstKey))
            {
                return firstKey.Value.GetAs<List<CharacterSaveData>>();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return null;
    }
    
    [Serializable]
    public class CharacterSaveData
    {
        public string CharacterId;
        public string Nickname;
        public int Level;
        public ClassType ClassType;
        public Gender Gender;
        public List<AppearanceSaveData> AppearanceSaveData = new List<AppearanceSaveData>();
    }

    [Serializable]
    public class AppearanceSaveData
    {
        public AppearanceType AppearanceType;
        public int Index;
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