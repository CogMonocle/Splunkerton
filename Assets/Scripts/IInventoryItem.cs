using UnityEngine;

public interface IInventoryItem
{

    Sprite GetInventorySprite();
}

public enum Attributes
{
    None,
    WeaponType,
    Sword
}
