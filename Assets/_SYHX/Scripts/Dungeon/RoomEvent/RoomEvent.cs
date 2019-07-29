using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEvent : MonoBehaviour
{
    protected bool HasHappend = false;
    public GameObject Front;
    public GameObject Back;
    //private static GenerateMap mapGenerator;
    //private static GameObject dungeonObject;
    // Start is called before the first frame update
    void Start()
    {
        //mapGenerator = gameObject.GetComponent<GenerateMap>();
        //dungeonObject = this.transform.parent.gameObject;
        //print(dungeonObject.name);
        Front.SetActive(false);
        Back.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void Event(int roomType)
    //{   //SHOULD BE DONE BY SONGLEI, I JUST MAKE A SIMPLE EXAMPLE HERE
    //    if (roomType ==1) //end room
    //    {
    //        endRoom();
    //    }
    //    if (roomType == 2)
    //    {
    //        battleRoom();
    //    }
    //}

    public virtual void EnterEvent() { }

    public virtual void LeaveEvent() { print("leave"); }

    public virtual void OnDetected()
    {
        Front.SetActive(true);
        Back.SetActive(false);
    }

}
