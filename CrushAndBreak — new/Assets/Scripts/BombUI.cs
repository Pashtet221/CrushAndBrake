using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bombUIText;


    private void Update()
    {
        UpdateBombsUIText();
    }

    public void UpdateBombsUIText()
    {
        SetBombsText(bombUIText, Bomb.instance.bombs);
    }

    void SetBombsText (TMP_Text textMesh, int value)
	{
		textMesh.text = value.ToString ();
	}
}
