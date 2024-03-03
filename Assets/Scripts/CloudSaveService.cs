using System;
using UnityEngine;

public class CloudSaveService : Singleton<CloudSaveService>
{
    public event Action Initialized; 
    public void Initialize()
    {
        Initialized?.Invoke();
        Debug.Log($"<color=green>{GetType().Name} initalized</color>");
    } 
}