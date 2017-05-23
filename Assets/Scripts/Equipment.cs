using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour, IInventoryItem
{
    public enum Slot
    {
        Head,
        Torso,
        Mainhand,
        Offhand,
        Twohand,
        Legs,
        Feet,
        Trinket
    }

    public Slot itemSlot;

    public Dictionary<PlayerController.Stats, int> stats;

    public int value;

    public Sprite inventorySprite;
    public Sprite equippedSprite;

    Sprite IInventoryItem.GetInventorySprite()
    {
        return inventorySprite;
    }
}
