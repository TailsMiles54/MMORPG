using System;
using Unity.Services.Authentication;
using UnityEngine;
using Zenject;

public class AuthPanelController : MonoBehaviour
{
    [SerializeField] private AuthPanelView _authPanelView;

    private AuthState _authState;

    [Inject] private AuthService _authService;

    public void Start()
    {
        AuthenticationService.Instance.SignOut();
        AuthenticationService.Instance.ClearSessionToken();
    }

    public async void Auth()
    {
        switch (_authState)
        {
            case AuthState.Login:
            {
                var login = _authPanelView.LoginTMP.text;
                var password = _authPanelView.PasswordTMP.text;
                await _authService.LoginWithUsernamePasswordAsync(login, password);
                break;
            }
            case AuthState.Register:
            {
                var login = _authPanelView.LoginTMP.text;
                var password = _authPanelView.PasswordTMP.text;
                var retryPassword = _authPanelView.RetryPasswordTMP.text;

                if (password == retryPassword)
                {
                    await _authService.RegisterWithUsernamePassword(login, password);
                }
                else
                {
                    _authPanelView.PasswordFieldShake();
                }
                break;
            }
        }
    }

    public void ChangeType()
    {
        switch (_authState)
        {
            case AuthState.Login:
                _authState = AuthState.Register;
                _authPanelView.RetryPasswordObject.SetActive(true);
                _authPanelView.CurrentButtonText.text = "Register";
                _authPanelView.ChangeButtonText.text = "Login";
                break;
            case AuthState.Register:
                _authState = AuthState.Login;
                _authPanelView.RetryPasswordObject.SetActive(false);
                _authPanelView.CurrentButtonText.text = "Login";
                _authPanelView.ChangeButtonText.text = "Register";
                break;
        }

        ClearFields();
    }

    public void ClearFields()
    {
        _authPanelView.LoginTMP.text = string.Empty;
        _authPanelView.PasswordTMP.text = string.Empty;
        _authPanelView.RetryPasswordTMP.text = string.Empty;
    }
    
    public enum AuthState
    {
        Login,
        Register
    }
}