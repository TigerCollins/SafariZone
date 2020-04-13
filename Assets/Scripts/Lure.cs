using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item,", menuName = "Items/Lure (CATCHING TOOLS)")]
public class Lure : Item
{
    [Header("Lure Specific Variables")]
    public float trapEffectiveness;
    public bool equipped;
    //LURES ARE CATCHING TOOLS 
}
