using UnityEngine;

public class CharacterEditor : MonoBehaviour
{
    [SerializeField] private GameObject _maleHead;
    [SerializeField] private GameObject _femaleHead;
    [SerializeField] private GameObject _maleBody;
    [SerializeField] private GameObject _femaleBody;

    private Gender _currentGender;

    private void Start()
    {
        SelectGender();
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

public enum Gender
{
    Male = 0,
    Female = 1,
}
