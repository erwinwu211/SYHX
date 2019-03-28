using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    public GameObject Room;
    public GameObject BlankRoom;
    public int[] maparray = new int[64];
	// Use this for initialization
	void Start () {
        foreach (var item in maparray.Select((v, i) => new { v, i }))
        {
            if (item.v == 1)
            {
                var room = Instantiate(Room, new Vector3((item.i % 8)*1.5f +0.5f ,0,Mathf.Floor(item.i/8)*1.5f +0.5f ), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
            else
            {
                var room = Instantiate(BlankRoom, new Vector3((item.i % 8) * 1.5f + 0.5f, 0, Mathf.Floor(item.i / 8) * 1.5f + 0.5f), Quaternion.identity);
                room.name = "Room " + item.i;
                room.transform.parent = this.transform;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
