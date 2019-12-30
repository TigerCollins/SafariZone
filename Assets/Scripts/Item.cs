using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Details")]
    public string itemName;
    public Sprite icon;
    public string flavourText;

    [Header("Inventory Details")]
    public int quantity;

    [Header("Shop Details")]
    public int price;

    public virtual void Use()
    {
        
    }
}
