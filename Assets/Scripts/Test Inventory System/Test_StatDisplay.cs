using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test_StatDisplay : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ValueText;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        NameText = texts[0];
        ValueText = texts[1];
    }
}
