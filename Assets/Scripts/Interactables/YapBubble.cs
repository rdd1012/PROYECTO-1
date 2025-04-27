using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class YapBubble : MonoBehaviour
{
    private TMP_Text text;
    void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }
    public void SetupText(string _text)
    {
        text.text = _text;
    } 
    
}
