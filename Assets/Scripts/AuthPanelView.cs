using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthPanelView : MonoBehaviour
{
    [field: SerializeField] public TMP_InputField LoginTMP { get; private set; }
    [field: SerializeField] public TMP_InputField PasswordTMP { get; private set; }
    [field: SerializeField] public TMP_InputField RetryPasswordTMP { get; private set; }
    [field: SerializeField] public GameObject RetryPasswordObject { get; private set; }
     
    [field: SerializeField] public TMP_Text CurrentButtonText { get; private set; }
    [field: SerializeField] public TMP_Text ChangeButtonText { get; private set; }
    
    public void PasswordFieldShake()
    {
        PasswordTMP.transform.DOShakePosition(0.2f,2);
        RetryPasswordTMP.transform.DOShakePosition(0.2f,2);
    }
}