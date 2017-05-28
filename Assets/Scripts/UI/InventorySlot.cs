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

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
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
            transform.SetParent(parentToReturnTo);
            transform.position = placeHolder.transform.position;
            transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
            group.blocksRaycasts = true;
            Destroy(placeHolder.gameObject);
            if (dropped != null && dropped.IsCompatible(GetItem().GetSlotType()))
            {
                Swap(dropped);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropped = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2 && HasItem())
        {
            if(slotType == SlotType.Inventory && item.GetSlotType() != SlotType.Inventory)
            {
                Swap(inventory.GetSlotByType(item.GetSlotType()));
            }
            if(slotType != SlotType.Inventory && inventory.Add(item))
            {
                SetItem(null);
            }
        }
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
