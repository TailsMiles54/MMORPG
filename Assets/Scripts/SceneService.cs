using UnityEngine.SceneManagement;
using Zenject;

public class SceneService : IInitializable
{
    [Inject] private AuthService _authService;
    [Inject] private CloudSaveService _cloudSaveService;
    [Inject] private PhotonService _photonService;
    [Inject] private FriendService _friendService;

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
            SceneManager.LoadSceneAsync("AuthScene");
        }
    }

    public void SelectCharacterScene()
    {
        SceneManager.LoadSceneAsync("CharacterSelect");
    }
}