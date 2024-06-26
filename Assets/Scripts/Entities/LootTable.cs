using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/LootTable")]
public class LootTable : ScriptableObject
{
    public ItemData[] tier0Loot;
    public int tier0Chance = 50;
    public ItemData[] tier1Loot;
    public int tier1Chance = 25;
    public ItemData[] tier2Loot;
    public int tier2Chance = 10;
    public ItemData[] tier3Loot;
    public int tier3Chance = 5;
    public ItemData[] tier4Loot;
    public int tier4Chance = 1;

    public ItemData GetRandomItem()
    {
        int random = Random.Range(0, 101);
        if(random <= tier4Chance)
        {
            return tier4Loot[Random.Range(0, tier4Loot.Length)];
        }else if (random <= tier3Chance)
        {
            return tier3Loot[Random.Range(0, tier3Loot.Length)];
        }else if (random <= tier2Chance)
        {
            return tier2Loot[Random.Range(0, tier2Loot.Length)];
        }
        else
        {
            return tier1Loot[Random.Range(0, tier1Loot.Length)];
        }
    }

}
