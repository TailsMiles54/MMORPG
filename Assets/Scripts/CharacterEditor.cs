using UnityEngine;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] private GameObject _maleHead;
    [SerializeField] private GameObject _femaleHead;
    [SerializeField] private GameObject _maleBody;
    [SerializeField] private GameObject _femaleBody;

    [SerializeField] private Scrollbar _scrollRectEye;
    [SerializeField] private Scrollbar _scrollRectMouth;
    [SerializeField] private Scrollbar _scrollRectHair;
    [SerializeField] private Scrollbar _scrollRectEyeBrow;
    
    [SerializeField] private GameObject[] _eyes;
    private GameObject _currentEye;
    
    private Gender _currentGender;

    private void Start()
    {
        SelectGender();
        _scrollRectEye.numberOfSteps = _eyes.Length-1;

        _scrollRectEye.onValueChanged.AddListener(EyeChange);
        _currentEye = _eyes[0];
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

    public void EyeChange(float test)
    {
        int currentStep = Mathf.RoundToInt(test / (1f / _scrollRectEye.numberOfSteps));
        _currentEye.SetActive(false);
        Debug.Log(currentStep);
        _currentEye = _eyes[currentStep];
        _currentEye.SetActive(true);
    }
}

public enum Gender
{
    Male = 0,
    Female = 1,
}
