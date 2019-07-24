using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEvent : MonoBehaviour
{
    private static GenerateMap mapGenerator;
    private static GameObject dungeonObject;
    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = gameObject.GetComponent<GenerateMap>();
        dungeonObject = this.transform.parent.gameObject;
        print(dungeonObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Event(int roomType)
    {   //SHOULD BE DONE BY SONGLEI, I JUST MAKE A SIMPLE EXAMPLE HERE
        if (roomType ==1) //end room
        {
            endRoom();
        }
        if (roomType == 2)
        {
            battleRoom();
        }
    }

    static void battleRoom()
    {
        print("battle");
        //save current scene TODO
        ES3.Save<GameObject>("dungeonObject", dungeonObject);
        SceneStatusManager.Ins.SetSceneStatus(new BattleStatus(SceneStatusManager.Ins));
    }

    static void endRoom()
    {
        print("end");
        mapGenerator.clearMap();
        mapGenerator.loadMap();
        
    }
}
