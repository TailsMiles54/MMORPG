using UnityEngine.SceneManagement;

public class SceneService : Singleton<SceneService>
{
    private AuthService _authService => AuthService.Instance;
    private CloudSaveService _cloudSaveService => CloudSaveService.Instance;
    private PhotonService _photonService => PhotonService.Instance;
    private FriendService _friendService => FriendService.Instance;

    private bool _authServiceLaunched;
    private bool _cloudSaveServiceLaunched;
    private bool _photonServiceLaunched;
    private bool _friendServiceLaunched;
    
    public void Initialize()
    {
        _authService.Initialized += () =>
        {
            _authServiceLaunched = true;
            CheckState();
        };
        _cloudSaveService.Initialized += () =>
        {
            _cloudSaveServiceLaunched = true;
            CheckState();
        };
        _photonService.Initialized += () =>
        {
            _photonServiceLaunched = true;
            CheckState();
        };
        _friendService.Initialized += () =>
        {
            _friendServiceLaunched = true;
            CheckState();
        };
    }

    public void CheckState()
    {
        if (_authServiceLaunched && _cloudSaveServiceLaunched && _photonServiceLaunched && _friendServiceLaunched)
        {
            SceneManager.LoadScene("AuthScene");
        }
    }
}