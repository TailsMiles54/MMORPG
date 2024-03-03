using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour
{
    public SceneService SceneServiceInstance { get; private set; }
    public AuthService AuthServiceInstance { get; private set; }
    public FriendService FriendServiceInstance { get; private set; }
    public CloudSaveService CloudSaveServiceInstance { get; private set; }
    public PhotonService PhotonServiceInstance { get; private set; }
    
    void Start()
    {
        SceneServiceInstance = new SceneService(); 
        AuthServiceInstance = new AuthService(); 
        FriendServiceInstance = new FriendService(); 
        CloudSaveServiceInstance = new CloudSaveService(); 
        PhotonServiceInstance = new PhotonService(); 
    }
}
