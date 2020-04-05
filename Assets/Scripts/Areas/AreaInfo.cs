using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaInfo : Area
{
    [System.Serializable]
    public struct Info
    {
        public Info(string name, int value, int maxValue)
        {
            this.name = name;
            this.value = value;
            this.valueMax = maxValue;
        }

        public string name;
        public int value;
        public int valueMax;
    }

    [SerializeField]
    public List<Info> InfoItems = new List<Info>();

    public void UpdateInfo()
    {
        string infoString = "";
        foreach (var item in InfoItems)
        {
            infoString += string.Format($"{item.name} : {item.value}/{item.valueMax} \n");
        }
        visual.SetAreaName(infoString);
    }

    public override void OnValidate()
    {
        base.OnValidate();
        try
        {
            visual.SetColor(GetComponentInParent<AreaRoom>().visual.areaColor);
        }
        catch
        {

        }
    }

    public override void Start()
    {
        base.Start();
        visual.SetColor(GetComponentInParent<AreaRoom>().visual.areaColor);
    }

    public override void Show()
    {
        base.Show();
        UpdateInfo();
    }

    public override bool Hover(AreaInteractable item)
    {
        //return base.Hover(item);
        return false;
    }
}
