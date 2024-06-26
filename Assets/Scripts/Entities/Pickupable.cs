using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PickupType
{
    Interact,
    Collision
}

public class Pickupable : MonoBehaviour
{
    private PickupType pickupType;
    [SerializeField] private Interactable interact;
    [SerializeField] private ItemData item;
    [SerializeField] private BoxCollider[] colliders;
    public void Initialize(ItemData item)
    {
        interact.interactLabel = item.itemName;
        GameObject model = Instantiate(item.modelPrefab, transform.position, Quaternion.identity, transform);
        this.item = item;
        pickupType = item.pickupType;
        if(item.pickupType != PickupType.Interact)
        {
            interact.enabled = false;
            gameObject.layer = 15;
        }
        MeshRenderer mesh = model.GetComponentInChildren<MeshRenderer>();
        foreach (BoxCollider col in colliders)
        {
            col.size = mesh.bounds.size;
            col.center = mesh.transform.localPosition;
        }

    }

    public void OnPickupGun(PlayerController player)
    {
        if (pickupType != PickupType.Interact) return;
        if (interact.curInteracter == null) return;
        player.h_weapon.PickupGun((GunData)item);
    }
    public void OnPickupAmmo(PlayerController player)
    {
        player.h_weapon.equippedGun.ammo += Random.Range(50, 251);
    }

    public void Pickup(PlayerController player)
    {
        switch(item.itemType)
        {
            case ItemType.Gun:
                OnPickupGun(player);
                break;
            case ItemType.Ammo:
                OnPickupAmmo(player);
                break;
        }
        Destroy(gameObject);
    }
    public void Pickup()
    {
        PlayerController player = interact.curInteracter.player;
        switch (item.itemType)
        {
            case ItemType.Gun:
                OnPickupGun(player);
                break;
            case ItemType.Ammo:
                OnPickupAmmo(player);
                break;
        }
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (pickupType != PickupType.Collision) return;
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            print("Player");
            PlayerController player = collision.collider.gameObject.GetComponent<PlayerController>();
            Pickup(player);
        }
    }

}
