using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WebSocketSharp;
using Zenject;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] private GameObject _maleHead;
    [SerializeField] private GameObject _femaleHead;
    [SerializeField] private GameObject _maleBody;
    [SerializeField] private GameObject _femaleBody;

    [SerializeField] private TMP_InputField _nameField;
    
    [SerializeField] private CharacterAppearanceController _characterAppearanceController;
    [SerializeField] private List<SliderAppearance> _slidersApeearanceList;
    
    private Gender _currentGender;

    [Inject] private CloudSaveService _cloudSaveService;
    [Inject] private CharacterSelectService _characterSelectService;

    private void Start()
    {
        SelectGender();
        foreach (var sliderAppearance in _slidersApeearanceList)
        {
            sliderAppearance.Start(_characterAppearanceController.GetAppearanceElementController(sliderAppearance.AppearanceType));
        }
    }

    public async void CreateCharacter()
    {
        var characters = _characterSelectService.Characters;

        if (_nameField.text.IsNullOrEmpty() || characters.Any(x => x.Nickname == _nameField.text))
        {
            _nameField.transform.DOShakePosition(1, 3);
            return;
        }
            
        var emptyCharacterData = characters.First(x => x.CharacterId.IsNullOrEmpty());

        emptyCharacterData.CharacterId = Guid.NewGuid().ToString();
        emptyCharacterData.Nickname = _nameField.text;

        emptyCharacterData.Gender = _currentGender;
        emptyCharacterData.ClassType = CloudSaveService.ClassType.TheAdventurer;
        
        foreach (var sliderAppearance in _slidersApeearanceList)
        {
            emptyCharacterData.AppearanceSaveData.Add(new CloudSaveService.AppearanceSaveData()
            {
                AppearanceType = sliderAppearance.AppearanceType,
                Index = sliderAppearance.CurrentIndex
            });
        }
        
        await _cloudSaveService.SaveCharacterData(characters);
    }

    private void SelectGender()
    {
        _maleHead.SetActive(_currentGender == Gender.Male);
        _maleBody.SetActive(_currentGender == Gender.Male);
        
        _femaleHead.SetActive(_currentGender == Gender.Female);
        _femaleBody.SetActive(_currentGender == Gender.Female);
    }

    public void SelectMale()
    {
        _currentGender = Gender.Male;
        SelectGender();
    }

    public void SelectFemale()
    {
        _currentGender = Gender.Female;
        SelectGender();
    }
}

[Serializable]
public class SliderAppearance
{
    [field: SerializeField] public Scrollbar Scrollbar { get; private set; }
    [field: SerializeField] public AppearanceType AppearanceType { get; private set; }
    private AppearanceElementController _appearanceElementController;

    public int CurrentIndex => Mathf.RoundToInt(Scrollbar.value / (1f / Scrollbar.numberOfSteps));

    public void Start(AppearanceElementController appearanceElementController)
    {
        _appearanceElementController = appearanceElementController;
        
        Scrollbar.numberOfSteps = _appearanceElementController.Objects.Length-1;

        Scrollbar.onValueChanged.AddListener((value) =>
        {
            int currentStep = CurrentIndex;
            _appearanceElementController.ChangePart(currentStep);
        });
    }

    ~SliderAppearance()
    {
        Scrollbar.onValueChanged.RemoveAllListeners();
    }
}

public enum Gender
{
    Male = 0,
    Female = 1,
}
