using UnityEngine;

public enum Attributes
{
    None,
    WeaponType,
    Sword
}

public interface IInventoryItem
{
    Sprite GetInventorySprite();
}
