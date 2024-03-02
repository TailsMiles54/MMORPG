using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AuthClick : MonoBehaviour
{
    [Inject] private AuthService _authService; 
    public void Test()
    {
        _authService.InitSignIn();
    }
}
