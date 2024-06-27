using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Properties")]
    public byte chanceForLoot = 50;

    [Header("Components")]
    public Health h_Health;
    public LootTableHandler h_lootTable;

    private void Awake()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        h_Health = this.SafeGetComponent(h_Health);
        h_lootTable = this.SafeGetComponent(h_lootTable);
    }

    public void OnDeath()
    {
        if(Random.Range(0, 100) <= chanceForLoot)
            h_lootTable.SpawnLoot();
        Destroy(gameObject);
    }

}
