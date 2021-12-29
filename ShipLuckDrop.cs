using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLuckDrop : MonoBehaviour
{
    public Loot_Table1 Table;
    PlayerInfo info;

    [SerializeField] float Cost;
    [SerializeField] float Refund;

    item Item;

    private void Awake()
    {
        info = FindObjectOfType<PlayerInfo>();

        
    }
    public void OnClick()
    {
       
        info.TotalCoins -= Cost;

        Item = Table.GetDrop();

        switch (Item.Rarity)
        {
            case item.RarityType.Common:
                Refund = 250;

                break;

            case item.RarityType.Uncommon:
                Refund = 500;

                break;

            case item.RarityType.Rare:
                Refund = 750;

                break;

            case item.RarityType.Epic:
                Refund = 1500;

                break;

            case item.RarityType.Legendary:
                Refund = 3000;

                break;

            case item.RarityType.Unique:
                Refund = 10000;

                break;

        }

        bool Duplication = false;

        for (int i = 0; i < info.UnlockedShips.Count; i++)
        {
            if (info.UnlockedShips[i].name == Item.name)
            {
                // Duplicate Detected
                Duplication = true;
                Duplicate();

                break; //don't need to check the remaining ones now that we found one
            }
                        
        }

        if (!Duplication)
            Reward();
       
    }

    void Duplicate()
    {

        info.TotalCoins += Refund;
        info.SavePlayer();

        Debug.Log("Item: " + Item.name + "Duplicate Detected");
    }

    void Reward()
    {
        info.UnlockedShips.Add(Item);

        Debug.Log("Item: " + Item.name + "Awarded");
        info.SavePlayer();
    }
}
