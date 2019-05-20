using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public GameObject DungeonGO;
    public GameObject DungeonInfoGO;
    public GameObject DungeonParent;
    private DungeonContent d01;
    private DungeonContent d02;
    private DungeonContent d03;
    private DungeonContent d04;
    private List<DungeonContent> dungeons;

    private void Start()
    {
        d01 = new Dungeon01().GenerateDungeon();
        d02 = new Dungeon01().GenerateDungeon();
        d03 = new Dungeon01().GenerateDungeon();
        d04 = new Dungeon01().GenerateDungeon();
        dungeons = new List<DungeonContent>();
        dungeons.Add(d01);
        dungeons.Add(d02);
        dungeons.Add(d03);
        dungeons.Add(d04);
        foreach (DungeonContent dc in dungeons)
        {
            GameObject go = GameObject.Instantiate(DungeonGO, dc.Pos, Quaternion.identity, DungeonParent.transform);
            go.transform.localPosition = dc.Pos;
        }

    }

    public void OnReturnBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new MainState(SceneStateManager.Ins));
    }
}
