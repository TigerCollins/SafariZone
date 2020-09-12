using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTankToggleClick : MonoBehaviour
{
    public int tankTypeBase;
    public void OnTankTypeToggleClick(int tankType)
    {
        if(tankType >= tankTypeBase)
        {
            tankTypeBase = tankType;
            
        }
    }
}