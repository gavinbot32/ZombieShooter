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
    [SerializeField] private LayerMask layerMask;
    private bool canDestroy = true;
    [SerializeField] private float lifeTime = 2;
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
        lifeTime *= 60f;

    }

    private void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        if(pickupType == PickupType.Collision)
        {
            Ray ray = new Ray(transform.position, Vector3.up);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction, Color.red,0.5f);

            if(Physics.Raycast(ray,out hit, 1f, layerMask))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
                    Pickup(player);
                }
            }
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
        canDestroy = !player.h_weapon.MaxAmmoCheck(player.h_weapon.equippedGun);
        player.h_weapon.AddAmmo(Random.Range(10, 75 ));
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
        if (canDestroy)
        {
            Destroy(gameObject);
        }
        canDestroy = true;
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
        if (canDestroy)
        {
            Destroy(gameObject);
        }
        canDestroy = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (pickupType != PickupType.Collision) return;
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.collider.gameObject.GetComponent<PlayerController>();
            Pickup(player);
        }

    }

}
