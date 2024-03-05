using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonView : MonoBehaviour
{
    [field: SerializeField] public TMP_Text Text {get; private set;}
    [field: SerializeField] public Button Button {get; private set;}
    [field: SerializeField] public Image Highlight {get; private set;}
}
