using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    public GameObject DungeonGO;
    public GameObject DungeonInfoGO;
    public GameObject DungeonParent;
    public GameObject Mask;
    private List<DungeonSource> dungeons;
    private Vector3 DungeonInfoHidePos;

    private void Start()
    {
        DungeonInfoHidePos = DungeonInfoGO.transform.localPosition;
        Mask.SetActive(false);
        dungeons = new List<DungeonSource>()
        {
            new Dungeon01(),
            new Dungeon02(),
            new Dungeon03(),
            new Dungeon04(),
        };

        foreach (DungeonSource  ds in dungeons)
        {
            GameObject go = GameObject.Instantiate(DungeonGO, ds.Pos, Quaternion.identity, DungeonParent.transform);
            go.transform.localPosition = ds.Pos;
            go.GetComponent<Button>().onClick.AddListener(delegate() {
                OnDungeonIconClick(ds);
            });
            go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = ds.Name;
        }

    }

    public void OnReturnBtnClick()
    {
        SceneStatusManager.Ins.SetSceneStatus(new MainStatus(SceneStatusManager.Ins));
    }

    public void OnDungeonIconClick(DungeonSource ds)
    {
        DungeonInfoGO.transform.localPosition = Vector3.zero;
        Mask.SetActive(true);
        RefreshDungeonInfo(ds);
    }

    public void OnDungeonInfoCloseBtnClick()
    {
        DungeonInfoGO.transform.localPosition = DungeonInfoHidePos;
        Mask.SetActive(false);
    }

    private void RefreshDungeonInfo(DungeonSource ds)
    {
        Transform tf = DungeonInfoGO.transform;
        TextMeshProUGUI name = tf.Find("Info/Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI desc = tf.Find("Info/Desc").GetComponent<TextMeshProUGUI>();
        name.text = ds.Name;
        desc.text = ds.Desc;
    }
}
