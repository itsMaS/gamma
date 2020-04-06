using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialProgressBar : MonoBehaviour
{

    float targetTicks;
    float estimate = 0;

    float fillTarget;

    [SerializeField]
    TextMeshProUGUI loadText;

    bool tracking = false;
    [SerializeField]
    Image img;
    private void Start()
    {
        GameManager.instance.OnTickPass += UpdateTarget;
        img.enabled = false;
    }

    public void StartCount(int FinishTicks, GameAction action)
    {
        loadText.text = action.doingPhrase;
        fillTarget = 0;
        img.fillAmount = 0;
        estimate = 0;
        targetTicks = FinishTicks;
        img.enabled = true;
        tracking = true;
    }

    void UpdateTarget(int ticks)
    {
        if (estimate >= targetTicks)
        {
            loadText.text = "";
            tracking = false;
            img.enabled = false;
            estimate = 0;
        }
        if(tracking)
        {
            estimate += ticks;
            fillTarget = Mathf.InverseLerp(0,targetTicks,estimate);
        }
    }

    private void Update()
    {
        img.fillAmount = Mathf.Lerp(img.fillAmount,fillTarget,0.05f);
    }

    private void OnDestroy()
    {
        GameManager.instance.OnTickPass -= UpdateTarget;
    }
}
