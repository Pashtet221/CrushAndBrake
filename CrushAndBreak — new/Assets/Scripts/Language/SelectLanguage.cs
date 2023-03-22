using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLanguage : MonoBehaviour
{
    public int language;


    private void Start()
    {
        language = PlayerPrefs.GetInt("language", language);
    }

    public void RussuanLanguage()
    {
        language = 0;
        PlayerPrefs.SetInt("language", language);
       // SceneManager.LoadScene("main");
    }

    public void EnglishLanguage()
    {
        language = 1;
        PlayerPrefs.SetInt("language", language);
       // SceneManager.LoadScene("main");
    }
}
