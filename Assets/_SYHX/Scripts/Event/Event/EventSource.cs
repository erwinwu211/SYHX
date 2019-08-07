using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSource : ScriptableObject
{
    public int id;
    public string title;
    public string desc;
    public Sprite pic;
    public List<OptionSource> optionSources;
    public int permanentCount = 0;//永久触发次数，为0则为可无限触发
    public int floorCount = 0;//单层触发次数，为0则为可无限触发
}
