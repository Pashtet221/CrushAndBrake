using UnityEngine;
using TMPro;

public class LanguageText : MonoBehaviour
{
    public int language;
    public string[] text;
    private TMP_Text textLine;


    private void Update()
    {
        language = PlayerPrefs.GetInt("language", language);
        textLine = GetComponent<TMP_Text>();
        textLine.text = "" + text[language];
    }
}
