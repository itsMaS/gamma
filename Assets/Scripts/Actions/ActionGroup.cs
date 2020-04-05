using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group", menuName = "Actions/Groups/Group")]
public class ActionGroup : ScriptableObject
{
    public List<GameAction> AvailableActions;
}
