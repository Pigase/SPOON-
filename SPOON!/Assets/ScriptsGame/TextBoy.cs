using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textBoy;
    [SerializeField] private string _textConsist;

    public void Text()
    {
        _textBoy.text = _textConsist;
    }
}
