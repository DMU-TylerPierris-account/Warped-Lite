using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot_Table1 : ScriptableObject
{
    [Serializable]
    public class Drop
    {
        public item drop;
        public int Chance;
    }

    public List<Drop> table;

    [NonSerialized]
    int totalChance = -1;
    public int Totalweight
    { 
        get
        {
            if(totalChance == -1)
            {
                CalculateTotalChance();
            }
            return totalChance;
        }
    }

    void CalculateTotalChance()
    {
        totalChance = 0;
        for(int i = 0; i < table.Count; i++)
        {
            totalChance += table[i].Chance;
        }
    }
    public item GetDrop()
    {
        int roll = UnityEngine.Random.Range(0, Totalweight);

        for (int i = 0; i < table.Count; i++)
        {
            roll -= table[i].Chance;
               
            if (roll < 0)
            {
                return table[i].drop;
            }
        }
        
        return table[0].drop;
    }
}
