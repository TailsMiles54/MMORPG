using DG.Tweening;
using TMPro;
using UnityEngine;

public class AuthPanelView : MonoBehaviour
{
    [field: SerializeField] public TMP_Text LoginTMP { get; private set; }
    [field: SerializeField] public TMP_Text PasswordTMP { get; private set; }
    [field: SerializeField] public TMP_Text RetryPasswordTMP { get; private set; }

    public void PasswordFieldShake()
    {
        PasswordTMP.transform.DOShakePosition(0.2f,2);
        RetryPasswordTMP.transform.DOShakePosition(0.2f,2);
    }
}