using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }       
    }

    public Action<AreaInteractable> OnShowActions;
    public void ShowActions(AreaInteractable interactable)
    {
        if (OnShowActions != null)
        {
            OnShowActions(interactable);
        }
    }

    public Action OnHideAreas;
    public void HideAreas()
    {
        if (OnHideAreas != null)
        {
            OnHideAreas();
        }
    }
}
