using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [System.Serializable]
    public struct PersonAttribute
    {
        public Person.AttributeTypes attributeType;
        public string attributeIconCode;
    }
    public PersonAttribute[] AttributesConstructor;

    [SerializeField]
    GameObject popup;

    [SerializeField]
    Color errorColor;
    [SerializeField]
    Color okColor;
    [SerializeField]
    Color bonusColor;
    [SerializeField]
    TMP_FontAsset font;

    private void Awake()
    {
        instance = this;
        Person.AttributeIconCodes.Clear();
        foreach (var item in AttributesConstructor)
        {
            Person.AttributeIconCodes.Add(item.attributeType,item.attributeIconCode);
        }
        GameAction.errorColor = "#"+ColorUtility.ToHtmlStringRGBA(errorColor);
        GameAction.okColor = "#"+ColorUtility.ToHtmlStringRGBA(okColor);
        GameAction.bonusColor = "#"+ColorUtility.ToHtmlStringRGBA(bonusColor);
    }

    public int dayLength = 100;

    private void Start()
    {
        TotalTime = 0;
    }

    [ContextMenu("Change project fonts")]
    public void ChangeAllFonts()
    {
        foreach (var item in FindObjectsOfType<TextMeshProUGUI>())
        {
            item.font = font;
        }
    }

    List<TrackedAction> TrackedActions = new List<TrackedAction>();
    public struct TrackedAction
    {
        public TrackedAction(int finishTime, Action onComplete)
        {
            this.finishTime = finishTime;
            this.OnComplete = onComplete;
        }
        public int finishTime;
        public Action OnComplete;
    }

    [ContextMenu("Start Time")]
    public void StartTime()
    {
        ticking = true;
        Debug.Log("--TIME TRACKING STARTED--");
        StartCoroutine(TimeControl());
    }
    [ContextMenu("Stop Time")]
    public void StopTime()
    {
        ticking = false;
        Debug.Log("--TIME TRACKING ENDED--");
        StopCoroutine(TimeControl());
    }

    public int day;
    int elapsedTicks;
    int targetTicks;

    public void StartTime(int ticks, Action onComplete)
    {
        TrackedActions.Add(new TrackedAction(TotalTime+ticks, onComplete));
        if (ticks > 0 && ticks > (targetTicks - elapsedTicks))
        {
            targetTicks = ticks;
            elapsedTicks = 0;
            if(!ticking)
            {
                StartTime();
            }
        }
    }
    public static void MakePopup(string message,Vector3 position)
    {
        Instantiate(GameManager.instance.popup, position, Quaternion.identity).GetComponent<Popup>().Initialize(message);
    }


    void TrackTime()
    {
        elapsedTicks += tickSize;
        if(elapsedTicks > targetTicks)
        {
            StopTime();
        }

        /*
        else
        {
            if((elapsedTicks + tickSize) > targetTicks)
            {
                tickSize = targetTicks - elapsedTicks;
            }
        }
        */

        foreach (var item in TrackedActions.FindAll(e => (e.finishTime <= TotalTime)))
        {
            item.OnComplete();
        }
        TrackedActions.RemoveAll(e => (e.finishTime <= TotalTime));

        day = TotalTime / dayLength;
        FindObjectOfType<DayFader>().SetDay(day);

    }

    public int TotalTime { get; private set; }

    public float tickTime;
    public int tickSize;
    bool ticking = false;

    public Action<int> OnTickPass;
    public Action OnTickPassVisual;
    IEnumerator TimeControl()
    {
        while(ticking)
        {
            yield return new WaitForSecondsRealtime(tickTime);
            TotalTime += tickSize;
            if (OnTickPass != null) { OnTickPass(tickSize); }
            yield return null;
            if (OnTickPassVisual != null) { OnTickPassVisual(); }
            TrackTime();
        }   
    }
}
