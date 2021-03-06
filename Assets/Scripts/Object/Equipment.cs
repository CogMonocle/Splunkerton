﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class StatDict : SerializableDictionary<Stats, int> { }
[Serializable] public class AttributeDict : SerializableDictionary<Attributes, Attributes> { }

public class Equipment : MonoBehaviour, IInventoryItem
{

    public SlotType itemSlot;

    public StatDict stats;
    public AttributeDict attributes;

    public int value;

    public Sprite inventorySprite;
    public Sprite equippedSprite;

    public void SetInfo(Equipment e)
    {
        if (e == null)
        {
            SetEmpty();
            return;
        }
        stats = e.stats;
        attributes = e.attributes;
        value = e.value;
        inventorySprite = e.inventorySprite;
        equippedSprite = e.equippedSprite;
        GetComponent<SpriteRenderer>().sprite = equippedSprite;
        transform.localPosition = new Vector3(e.transform.localPosition.x, e.transform.localPosition.y, transform.localPosition.z);
        transform.localRotation = e.transform.localRotation;
        transform.localScale = e.transform.localScale;
    }

    public void SetEmpty()
    {
        stats = new StatDict();
        attributes = new AttributeDict();
        value = -1;
        inventorySprite = null;
        equippedSprite = null;
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public Sprite GetInventorySprite()
    {
        return inventorySprite;
    }

    public SlotType GetSlotType()
    {
        return itemSlot;
    }
}
