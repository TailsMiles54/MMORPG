using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAppearanceController : MonoBehaviour
{
    [SerializeField] private List<AppearanceElementController> _appearanceElementControllers;

    private void Awake()
    {
        foreach (var appearanceElementController in _appearanceElementControllers)
        {
            appearanceElementController.Start();
        }
    }

    public AppearanceElementController GetAppearanceElementController(AppearanceType appearanceType)
    {
        return _appearanceElementControllers.First(x => x.AppearanceType == appearanceType);
    }

    public void SetupFromCharacterData(Gender gender, List<CloudSaveService.AppearanceSaveData> appearanceSaveData)
    {
        foreach (var appearanceData in appearanceSaveData)
        {
            var appearancePart =
                GetAppearanceElementController(appearanceData.AppearanceType);
            
            appearancePart.ChangePart(appearanceData.Index);
        }
        
        GetAppearanceElementController(AppearanceType.Body).ChangePart(gender == Gender.Male ? 0 : 1);
        GetAppearanceElementController(AppearanceType.Body).ChangePart(gender == Gender.Male ? 0 : 1);
    }
}
