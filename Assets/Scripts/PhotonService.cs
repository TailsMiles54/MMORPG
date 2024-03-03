using System;
using UnityEngine;

public class PhotonService : Singleton<PhotonService>
{
    public event Action Initialized; 
    public void Initialize()
    {
        Initialized?.Invoke();
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    }
}