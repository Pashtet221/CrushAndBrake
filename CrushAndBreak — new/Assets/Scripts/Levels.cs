using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Level
{
    public string levelName;
    public GameObject levelGameObject;
    public int prefabsAmount;
    public GameObject[] prefabs;
}


public class Levels : MonoBehaviour
{
    public Level[] lvls;

    private Level currentLevel;
    private int currentLevelNumber;

    private bool canSpawn = true;


    [SerializeField] GameObject[] levels;
    private int _level = 0;

    [SerializeField] private Button nextLevelButton;

    [SerializeField] private GameObject[] UI;
    [SerializeField] private GameObject button;
    [SerializeField] private RectTransform timer;


    private void Start()
    {
        ChangeLevel();
        HideNextLevelButton();
    }

    private void OnEnable()
    {
        nextLevelButton.onClick.AddListener(NewLevel);
    }

    private void OnDisable()
    {
        nextLevelButton.onClick.RemoveListener(NewLevel);
    }

    private void Update()
    {
        currentLevel = lvls[currentLevelNumber];
        SpawnWave();
        GameObject[] totalPrefabs = GameObject.FindGameObjectsWithTag("Prefab");

        currentLevel.prefabsAmount = totalPrefabs.Length;


        if (totalPrefabs.Length == 0  )
        {
            if ( currentLevelNumber + 1 != lvls.Length )
            {
               DisplayNextLevelButton();
            }
            else
            {
                Debug.Log("GameFinish");
            }
        }
    }


    void SpawnWave()
    {
        if (canSpawn)
        {
            currentLevel.prefabsAmount--;
            if (currentLevel.prefabsAmount == 0)
            {
                canSpawn = false;
            }
        }
        
    }

    private void ChangeLevel() 
    {
        levels[_level].SetActive(true);

        for (int i = 0; i < levels.Length; i++)
        {
            if (i != _level)
            {
                levels[i].SetActive (false);
            }     
        }  
    }

   public void NewLevel()
   {
        _level++;
        ChangeLevel();
        HideNextLevelButton();
   }


   private void DisplayNextLevelButton()
   {
       for (int i = 0; i < UI.Length; i++)
       {
			UI[i].SetActive(false);
	   }
       button.SetActive(true);

       timer.DOAnchorPos(new Vector2(100, 49), 0.25f);
   }

   private void HideNextLevelButton()
   {
       for (int i = 0; i < UI.Length; i++)
       {
			UI[i].SetActive(true);
	   }
       button.SetActive(false);
       timer.DOAnchorPos(new Vector2(-56f, 49), 0.25f);
   }
}
