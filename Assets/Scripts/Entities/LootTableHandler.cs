using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTableHandler : MonoBehaviour
{
    public Pickupable pickupPrefab;

    public LootTable lootTable;

    public void SpawnLoot()
    {
        ItemData item = lootTable.GetRandomItem();
        Pickupable pu = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        pu.Initialize(item);
    }

}
