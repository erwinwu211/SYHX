using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : SingletonMonoBehaviour<MapUI>
{
    public GameObject DungeonGO;
    public GameObject DungeonInfoGO;
    public GameObject DungeonParent;
    public GameObject Mask;
    public GameObject ChapterGO;
    public GameObject ChapterParent;
    public Color Normal;
    public Color ChapterSelected;

    /// <summary>
    /// 加载并刷新左侧章节列表的信息
    /// </summary>
    /// <param name="chapters">章节列表</param>
    public void LoadChaptersInfo(List<Chapter> chapters)
    {
        foreach (Chapter cp in chapters)
        {
            //生成
            GameObject go = GameObject.Instantiate(ChapterGO, ChapterParent.transform);
            go.SetActive(true);
            RefreshChapterGO(cp, go);
            go.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnChapterToggleChanged(cp,go,value));
        }
    }

    public void OnChapterToggleChanged(Chapter chapter,GameObject go, bool value)
    {
        if (value)
        {
            RefreshDungeonGO(chapter);
        }
    }

    /// <summary>
    /// 刷新单个章节选项的信息
    /// </summary>
    /// <param name="chapter">单个章节</param>
    /// <param name="go">GameObject</param>
    private void RefreshChapterGO(Chapter chapter,GameObject go)
    {
        //获取各个节点
        Text name = go.transform.Find("Name").GetComponent<Text>();
        Text name_en = go.transform.Find("English").GetComponent<Text>();
        Transform bg = go.transform.Find("Bg");
        Transform triggle = go.transform.Find("Triggle");
        //进行赋值操作
        name.text = chapter.ChapterName;
        name_en.text = chapter.ChapterEnglishName;
        bg.gameObject.SetActive(false);
        triggle.gameObject.SetActive(false);
        if (chapter.IsDefault)
        {
            name.color = ChapterSelected;
            name_en.color = ChapterSelected;
            bg.gameObject.SetActive(true);
            triggle.gameObject.SetActive(true);
            OnChapterToggleChanged(chapter, go, true);
        }
    }

    /// <summary>
    /// 刷新所选章节所包含的关卡
    /// </summary>
    /// <param name="chapter">所选章节</param>
    private void RefreshDungeonGO(Chapter chapter)
    {
        foreach (Transform tf in DungeonParent.transform)
        {
            Destroy(tf.gameObject);
        }
        foreach (Dungeon dungeon in chapter.Dungeons)
        {
            GameObject go = Instantiate(DungeonGO,DungeonParent.transform);
            go.transform.localPosition = dungeon.Pos;
            Text index = go.transform.Find("NameFrame/Index").GetComponent<Text>();
            Text name = go.transform.Find("NameFrame/Name").GetComponent<Text>();
            index.text = dungeon.DungeonIndex;
            name.text = dungeon.DungeonName;
            go.GetComponent<Button>().onClick.AddListener(delegate()
            {
                ShowDungeonInfo(dungeon);
                ChooseStatus status = SceneStatusManager.Ins.current as ChooseStatus;
                status.Dungeon = dungeon;
            });
        }
    }

    /// <summary>
    /// 显示关卡详情
    /// </summary>
    /// <param name="dungeon"></param>
    public void ShowDungeonInfo(Dungeon dungeon)
    {
        Mask.SetActive(true);
        DungeonInfoGO.transform.localPosition = Vector3.zero;
        Image chapter_image = DungeonInfoGO.transform.Find("Info/Chapter").GetComponent<Image>();
        Text name = DungeonInfoGO.transform.Find("Info/Name").GetComponent<Text>();
        Text name_en = DungeonInfoGO.transform.Find("Info/Name_EN").GetComponent<Text>();
        Text status = DungeonInfoGO.transform.Find("Info/Status").GetComponent<Text>();
        chapter_image.sprite = dungeon.Chapter.Sprite;
        name.text = dungeon.DungeonName;
        name_en.text = dungeon.DungeonName_EN;
    }

    /// <summary>
    /// 隐藏关卡详情
    /// </summary>
    public void EnhanceDungeonInfo()
    {
        Mask.SetActive(false);
        DungeonInfoGO.transform.position = new Vector3(-1553.4f, -64);
    }

    /// <summary>
    /// 返回按钮事件
    /// </summary>
    public void OnReturnBtnClick()
    {
        SceneStatusManager.Ins.SetSceneStatus(new MainStatus(SceneStatusManager.Ins));
    }

    public void OnStartBtnClick()
    {
        ChooseStatus status = SceneStatusManager.Ins.current as ChooseStatus;
        status.GoToDungeonStatus();
    }

    protected override void UnityAwake()
    {
    }
}
