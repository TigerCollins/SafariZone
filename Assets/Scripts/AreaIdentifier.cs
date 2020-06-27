using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaIdentifier : MonoBehaviour
{
    public int areaID;
    [TextArea(1,2)]public string areaName;
    public Sprite areaIcon;
    public bool changeMovementTracker;

}
