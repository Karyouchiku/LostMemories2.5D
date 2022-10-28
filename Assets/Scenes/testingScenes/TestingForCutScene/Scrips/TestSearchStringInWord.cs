using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TestSearchStringInWord : MonoBehaviour
{
    public TMP_Text text;
    string searchWord;
    public string WordToChange;
    void Start()
    {
        searchWord = @"\bBurito\b";

        text.text = Regex.Replace(text.text,searchWord, WordToChange);
    }
}
