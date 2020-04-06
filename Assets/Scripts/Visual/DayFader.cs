using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayFader : MonoBehaviour
{
    [SerializeField]
    float fadeDuration = 5;

    [SerializeField]
    TextMeshProUGUI dayText;
    [SerializeField]
    CanvasGroup overlay;

    int currentDay = 0;

    public void Start()
    {
        dayText.text = string.Format($"Day : {1}");
        overlay.alpha = 1;
        overlay.LeanAlpha(0, fadeDuration).setEaseInOutExpo();
    }

    public void SetDay(int day)
    {
        if(currentDay != day)
        {
            ShowOverlay();
        }
        currentDay = day;
    }

    void ShowOverlay()
    {
        dayText.text = string.Format($"Day : {currentDay+2}");
        overlay.alpha = 0;
        overlay.LeanAlpha(1,2).setLoopPingPong(1);
    }
}
