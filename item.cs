using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class item : ScriptableObject
{
    public enum ItemType
    {
        EMPTY,
        Obstical,
        Ship
    }

    public enum RarityType
    {
        EMPTY = 0,
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5,
        Unique = 6
    }

    public string Name;
    public int ID;
    public GameObject Object;
    public ItemType type;
    public RarityType Rarity;
    
    private void Start()
    {
        Name = "";
        ID = 0;
        Object = null;
        type = ItemType.EMPTY;
        Rarity = RarityType.EMPTY;
    }
}
