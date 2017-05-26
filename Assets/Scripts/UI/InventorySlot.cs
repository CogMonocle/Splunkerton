using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;


public enum SlotType
{
    Head,
    Torso,
    Mainhand,
    Offhand,
    Twohand,
    Legs,
    Feet,
    Trinket,
    Inventory,
    Trinket2
}

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    static InventorySlot dropped;

    Image image;
    RectTransform rectTransform;
    IInventoryItem item;
    CanvasGroup group;
    Transform parentToReturnTo;
    InventorySlot placeHolder;
    Inventory inventory;

    public Sprite emptySprite;
    public SlotType slotType = SlotType.Inventory;

    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        inventory = GetComponentInParent<Inventory>();
        SetSprite(emptySprite);
    }

    public void SetItem(IInventoryItem i)
    {
        item = i;
        if (item != null)
        {
            SetSprite(item.GetInventorySprite());
            if (slotType != SlotType.Inventory)
            {
                inventory.Equip(i as Equipment, slotType);
            }
        }
        else
        {
            SetSprite(emptySprite);
            if (slotType != SlotType.Inventory)
            {
                inventory.Equip(i as Equipment, slotType);
            }
        }
    }

    public IInventoryItem GetItem()
    {
        return item;
    }

    public bool HasItem()
    {
        return item != null;
    }

    public void SetSprite(Sprite s)
    {
        if (s == null)
        {
            image.color = Color.clear;
        }
        else
        {
            image.color = Color.white;
            image.sprite = s;
        }
    }

    public void Swap(InventorySlot slot)
    {
        if (slot != null)
        {
            IInventoryItem temp = item;
            SetItem(slot.GetItem());
            slot.SetItem(temp);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (HasItem())
        {
            dropped = null;
            parentToReturnTo = transform.parent;
            placeHolder = Instantiate(this, parentToReturnTo);
            placeHolder.name = name;
            placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
            transform.SetParent(parentToReturnTo.parent);
            group.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (HasItem())
        {
            rectTransform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (HasItem())
        {
            Swap(placeHolder);
            if (dropped != null && dropped.IsCompatible(placeHolder.GetItem().GetSlotType()))
            {
                placeHolder.Swap(dropped);
            }
            Destroy(gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropped = this;
    }

    public bool IsCompatible(SlotType itemType)
    {
        if (slotType == SlotType.Inventory)
        {
            return true;
        }
        if (slotType == itemType)
        {
            return true;
        }
        return false;
    }
}
