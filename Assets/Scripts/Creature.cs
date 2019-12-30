using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class Creature : ScriptableObject
{
    [Header("Creature Details")]
    public bool previouslyCaptured;
    public string creatureName;
    public Sprite creatureIcon;
    public Sprite habitatIcon;
    public string flavourText;
    public int totalCaught;
    public string dateFirstCaught;
    public float heaviestCaught;

    [Header("Shop Details")]
    public int price;
    public enum CreatureType { Extraordinary, Earth, Monster, Leaf, Bird, Endearing, Phantom }
    public CreatureType creatureType;
    public enum CreatureRarity {Common, Uncommon, Rare, Impossible}
    public CreatureRarity creatureRarity;

    public virtual void Use()
    {
        
    }
}
