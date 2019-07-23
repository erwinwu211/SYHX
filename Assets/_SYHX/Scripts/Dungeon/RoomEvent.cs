using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }


    static void endRoom()
    {
        print("end");
    }
}
