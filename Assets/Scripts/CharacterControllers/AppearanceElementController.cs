using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

[Serializable]
public class AppearanceElementController
{
    [field: SerializeField] public AppearanceType AppearanceType { get; private set; }
    [field: SerializeField] public GameObject[] Objects { get; private set; }
    private GameObject _current;
    
    public void Start()
    {
        foreach (var element in Objects)
        {
            element.SetActive(false);
        }

        _current = Objects.First();
        _current.SetActive(true);
    }

    public void ChangePart(int index)
    {
        if(Objects.IndexOf(_current) == index)
            return;
        
        _current.SetActive(false);
        _current = Objects[index];
        _current.SetActive(true);
    }
}

public enum AppearanceType
{
    Eye = 0,
    Mouth = 2,
    Hair = 3,
    
    Body = 4,
    Head = 5,
}
