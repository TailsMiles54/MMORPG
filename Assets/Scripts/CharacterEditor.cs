using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] private GameObject _maleHead;
    [SerializeField] private GameObject _femaleHead;
    [SerializeField] private GameObject _maleBody;
    [SerializeField] private GameObject _femaleBody;

    [SerializeField] private CharacterAppearanceController _characterAppearanceController;
    [SerializeField] private List<SliderAppearance> _slidersApeearanceList;
    
    private Gender _currentGender;

    private void Start()
    {
        SelectGender();
        foreach (var sliderAppearance in _slidersApeearanceList)
        {
            sliderAppearance.Start(_characterAppearanceController.GetAppearanceElementController(sliderAppearance.AppearanceType));
        }
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
    [SerializeField] private Scrollbar _scrollbar;
    [field: SerializeField] public AppearanceType AppearanceType { get; private set; }
    private AppearanceElementController _appearanceElementController;

    public void Start(AppearanceElementController appearanceElementController)
    {
        _appearanceElementController = appearanceElementController;
        
        _scrollbar.numberOfSteps = _appearanceElementController.Objects.Length-1;

        _scrollbar.onValueChanged.AddListener((value) =>
        {
            int currentStep = Mathf.RoundToInt(value / (1f / _scrollbar.numberOfSteps));
            _appearanceElementController.ChangePart(currentStep);
        });
    }

    ~SliderAppearance()
    {
        _scrollbar.onValueChanged.RemoveAllListeners();
    }
}

public enum Gender
{
    Male = 0,
    Female = 1,
}
