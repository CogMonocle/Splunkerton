using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    RectTransform rectTransform;
    bool visible;

    public float movementRate;
    public float visiblePosition;
    public float hiddenPosition;
    public int inventoryWidth;
    public int inventoryHeight;

    public InventorySlot slotPrefab;
    public InventorySlot mainhandSlot;
    public InventorySlot torsoSlot;
    public InventorySlot[] mainInventory;

    public Equipment playerMainhand;
    public Equipment playerTorso;

    public Equipment ironSword;
    public Equipment ironChestplate;
    public Equipment redSword;

    public RectTransform backpack;

    public

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        visible = false;
        mainInventory = new InventorySlot[inventoryHeight * inventoryWidth];
        for (int i = 0; i < inventoryHeight; i++)
        {
            for (int j = 0; j < inventoryWidth; j++)
            {
                mainInventory[i * inventoryWidth + j] = Instantiate(slotPrefab, backpack);
            }
        }
        mainhandSlot.SetItem(ironSword);
        torsoSlot.SetItem(ironChestplate);
        Add(redSword);
    }

    void FixedUpdate()
    {
        if (visible)
        {
            MoveTowardsPosition(visiblePosition);
        }
        else
        {
            MoveTowardsPosition(hiddenPosition);
        }
    }

    void MoveTowardsPosition(float target)
    {
        Vector3 position = rectTransform.anchoredPosition;
        position.x = (target - position.x) * movementRate + position.x;

        rectTransform.anchoredPosition = position;
    }

    public void ToggleVisible()
    {
        visible = !visible;
    }

    public void Equip(Equipment e, SlotType slot)
    {
        Equipment equipment = null;
        switch (slot)
        {
            case SlotType.Mainhand:
                equipment = playerMainhand;
                break;
            case SlotType.Torso:
                equipment = playerTorso;
                break;
            default:
                return;
        }
        equipment.SetInfo(e);
    }

    public InventorySlot GetSlotByType(SlotType slot)
    {
        switch (slot)
        {
            case SlotType.Mainhand:
                return mainhandSlot;
            case SlotType.Torso:
                return torsoSlot;
            default:
                return mainInventory[0];
        }
    }

    public bool Add(IInventoryItem item)
    {
        foreach (InventorySlot s in mainInventory)
        {
            if (!s.HasItem())
            {
                s.SetItem(item);
                return true;
            }
        }
        return false;
    }
}
