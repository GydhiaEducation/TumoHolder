using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TextUI : MonoBehaviour
{
    TextMeshProUGUI _text;

    #region Event Subscription

    void OnEnable()
    {
        transform.parent.GetComponent<IDrawInterface>().OnDrawText += ChangeText;
        transform.parent.GetComponent<IDrawInterface>().OnDrawCanvas += DrawText;
    }

    void OnDisable()
    {
        transform.parent.GetComponent<IDrawInterface>().OnDrawText -= ChangeText;
        transform.parent.GetComponent<IDrawInterface>().OnDrawCanvas -= DrawText;
    }

    #endregion

    #region Initialization

    void Awake()
    {
        if (transform.GetComponentInChildren<TextMeshProUGUI>())
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _text.gameObject.SetActive(false);
        }
    }

    #endregion

    public void ChangeText(string textToUse)
    {
        if (_text != null)
            _text.text = textToUse;
    }

    public void DrawText(bool draw)
    {
        if (_text != null)
            _text.gameObject.SetActive(draw);
    }
}