using Unity.Services.Authentication;
using UnityEngine;

public class AuthPanelController : MonoBehaviour
{
    [SerializeField] private AuthPanelView _authPanelView;

    private AuthState _authState;

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
                await AuthService.Instance.LoginWithUsernamePasswordAsync(login, password);
                break;
            }
            case AuthState.Register:
            {
                var login = _authPanelView.LoginTMP.text;
                var password = _authPanelView.PasswordTMP.text;
                var retryPassword = _authPanelView.RetryPasswordTMP.text;

                if (password == retryPassword)
                {
                    await AuthService.Instance.RegisterWithUsernamePassword(login, password);
                }
                else
                {
                    _authPanelView.PasswordFieldShake();
                }
                break;
            }
        }
    }

    public enum AuthState
    {
        Login,
        Register
    }
}