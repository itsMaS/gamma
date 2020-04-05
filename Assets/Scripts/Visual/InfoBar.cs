using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBar : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI info;
    [SerializeField]
    Person person;

    private void Start()
    {
        GameManager.instance.OnTickPassVisual += UpdateInfo;
    }

    private void Update()
    {
        string textBuffer = "";
        foreach (var item in Person.AttributeIconCodes)
        {
            textBuffer += string.Format($"{Person.AttributeIconCodes[item.Key]}:{person.Attributes[item.Key]}   ");
        }


        info.text = textBuffer;
    }

    void UpdateInfo()
    {

    }
}
