using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private Interactable interact;
    [SerializeField] private ItemData item;
    public void Initialize(ItemData item)
    {
        interact.interactLabel = item.itemName;
        Instantiate(item.modelPrefab, transform.position, Quaternion.identity, transform);
        this.item = item;
    }

    public void OnPickup()
    {
        if (interact.curInteracter == null) return;
        interact.curInteracter.player.h_weapon.PickupGun((GunData)item);
        Destroy(gameObject);
    }

}
