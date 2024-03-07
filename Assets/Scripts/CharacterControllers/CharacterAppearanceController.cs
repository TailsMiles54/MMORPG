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
}
