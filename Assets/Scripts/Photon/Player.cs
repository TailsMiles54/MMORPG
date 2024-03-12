using System;
using System.Linq;
using Fusion;
using Photon;
using UnityEngine;
using Zenject;

public class Player : NetworkBehaviour
{
    [SerializeField] private CharacterAppearanceController _characterAppearanceController;
    
    private NetworkCharacterController _cc;
    
    [Inject] private CloudSaveService _cloudSaveService;
    [Inject] private CharacterService _characterService;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    private async void Start()
    {
        var id = _characterService.CurrentCharacter;
        var characters = await _cloudSaveService.LoadCharacters();
        var characterData = characters.First(x => x.CharacterId == id);
        _characterAppearanceController.SetupFromCharacterData(characterData.Gender, characterData.AppearanceSaveData);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.Direction.Normalize();
            _cc.Move(5*data.Direction*Runner.DeltaTime);
        }
    }
}
