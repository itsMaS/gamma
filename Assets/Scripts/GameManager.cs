using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct PersonAttribute
    {
        public Person.AttributeTypes attributeType;
        public string attributeIconCode;
    }
    public PersonAttribute[] AttributesConstructor;

    [SerializeField]
    Color errorColor;
    [SerializeField]
    Color okColor;
    [SerializeField]
    TMP_FontAsset font;

    private void Awake()
    {
        foreach (var item in AttributesConstructor)
        {
            Person.AttributeIconCodes.Add(item.attributeType,item.attributeIconCode);
        }
        GameAction.errorColor = "#"+ColorUtility.ToHtmlStringRGBA(errorColor);
        GameAction.okColor = "#"+ColorUtility.ToHtmlStringRGBA(okColor);
    }



    [ContextMenu("Change project fonts")]
    public void ChangeAllFonts()
    {
        foreach (var item in FindObjectsOfType<TextMeshProUGUI>())
        {
            item.font = font;
        }
    }
}
