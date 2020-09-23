using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemTypes {Lure, Whistle, Incense }

    [Header("Item Details")]
    public string itemName;
    public Sprite icon;
    [TextArea(2, 4)]
    public string flavourText;
    public ItemTypes itemTypes;
    public bool canHaveMultiple;
    public float lingerTime;
    public bool travelPass;
    public bool isOwned;
    public int amountOwnedLifetime;

    [Header("Inventory Details")]
    public int quantity;

    [Header("Shop Details")]
    public int price;

    [Header("Weight Specific Variables")]
    public float smallWeightFactor;
    public float largeWeightFactor;

    public virtual void Use()
    {
        
    }



}
