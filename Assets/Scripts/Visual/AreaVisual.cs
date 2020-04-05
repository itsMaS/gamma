using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaVisual : MonoBehaviour
{
    [Header("Dependacies")]
    [SerializeField]
    SpriteRenderer background;
    [SerializeField]
    SpriteRenderer outline;
    [SerializeField]
    BoxCollider2D box;
    [SerializeField]
    GameObject area;
    [SerializeField]
    Transform mask;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    TextMeshProUGUI areaName;
    [SerializeField]
    TextMeshProUGUI areaMessage;
    [SerializeField]
    RectTransform childrenHolder;

    [Header("Parameters")]
    public Color areaColor;
    [SerializeField]
    float height;
    [SerializeField]
    float width;
    [SerializeField]
    bool scroll;
    [SerializeField]
    bool isAction;

    public void UpdateArea()
    {
        if(width > 0 && height > 0)
        {
            if (scroll)
            {
                background.size = new Vector2(width, height * 2);
            }
            else
            {
                background.size = new Vector2(width, height);
            }

            mask.localScale = new Vector3(width, height, 1);
            outline.size = new Vector2(width, height);
            box.size = new Vector2(width, height);
        }

        background.color = areaColor;
        outline.color = areaColor;
        RectTransform rctr = canvas.GetComponent<RectTransform>();
        if (isAction)
        {
            rctr.sizeDelta = new Vector2(0, 0);
        }
        else
        {
            rctr.sizeDelta = new Vector2(width, height);
        }
        if(childrenHolder)
        {
            childrenHolder.sizeDelta = new Vector2(width, height);
        }
    }

    void BackgroundLoop()
    {
        background.transform.localPosition = new Vector3(0, height * 0.5f, 0);
        background.transform.LeanMoveLocalY(-0.5f * height, 5f).setOnComplete(BackgroundLoop);
    }

    private void OnValidate()
    {
        UpdateArea();
    }

    private void Start()
    {
        if(scroll)
        {
            BackgroundLoop();
        }
    }

    public void SetAreaName(string name)
    {
        areaName.text = name;
        UpdateArea();
    }
    public void SetAreaMessage(string mesg)
    {
        areaMessage.text = mesg;
    }
    public void SetDimensions(Vector3 position, float width, float height)
    {
        transform.position = position;
        this.width = width;
        this.height = height;

        UpdateArea();
    }
    public void SetColor(Color color)
    {
        areaColor = color;
        UpdateArea();
    }
    public void PingText()
    {
        areaName.transform.LeanScale(Vector3.one * 1.1f,0.1f).setLoopPingPong(1);
        areaMessage.transform.LeanScale(Vector3.one * 1.1f,0.1f).setLoopPingPong(1);
    }
    public void InflateMessage()
    {
        areaMessage.transform.LeanScale(Vector3.one * 1.1f, 0.3f);
    }
    public void DeflateMessage()
    {
        areaMessage.transform.LeanScale(Vector3.one * 1.0f, 0.3f);
    }

}
