using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string BattleSceneName= "Battle Scene";
    private string MainSceneName="Main";

    // Use this for initialization
    void Start()
    {
        
    }


    void Update()
    {
     //For DEBUG
     if (Input.GetKeyDown(KeyCode.M))
        {
            LoadMain();
        }
     if (Input.GetKeyDown(KeyCode.B))
        {
            LoadBattle();
        }
    }

    //Loading battle scene
    public void LoadBattle()
    {
        SceneManager.LoadScene(BattleSceneName);
    }

    //Loading Main scene
    public void LoadMain()
    {
        SceneManager.LoadScene(MainSceneName);
    }

}
