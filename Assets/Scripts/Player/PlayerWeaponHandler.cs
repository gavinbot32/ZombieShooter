using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    public PlayerController controller;
    public Gun equippedGun;
    private int gunIndex;
    public List<Gun> gunList;
    public int maxGuns = 2;

    [SerializeField] private Transform gunContainer;
    [SerializeField] private Pickupable pickupPrefab;

    private void Start()
    {
        Equip(equippedGun);
    }

    public void PickupGun(GunData gun)
    {
        if (gunList.Count >= maxGuns)
        {
            
            gunList.Remove(equippedGun);
            
            Pickupable pu = Instantiate(pickupPrefab, gunContainer.position, Quaternion.identity);
            pu.Initialize(equippedGun.gunData);
            Destroy(equippedGun.gameObject);
        }
        Gun newGun = Instantiate(gun.gunPrefab, gunContainer);
        gunList.Add(newGun);
        Equip(newGun);
    }

    public void Equip(Gun gun) {
        equippedGun.gameObject.SetActive(false);
        equippedGun = gun;
        equippedGun.gameObject.SetActive(true);
        equippedGun.Initialize(controller);
        controller.h_hud.UpdateGunText();
    }
    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (equippedGun == null) return;
        if (equippedGun.gunData.type == GunType.Automatic)
        {
            if (context.performed)
            {
                equippedGun.holding = true;
            }
            if (context.canceled)
            {
                equippedGun.holding = false;
            }
        } else if (equippedGun.gunData.type == GunType.SemiAutomatic)
        {
            if (context.performed)
            {
                equippedGun.Fire();
            }
        }
    }

    public void OnReloadInput(InputAction.CallbackContext context)
    {
        if (equippedGun == null) return;
        if (context.performed)
        {
            equippedGun.Reload();
        }
    }
    public void OnCycleInputUp(InputAction.CallbackContext context)
    {
        CycleWeapon(1);
    }
    public void OnCycleInputDown(InputAction.CallbackContext context)
    {
        CycleWeapon(-1);
    }

    public void CycleWeapon(int modifier = 1)
    {
        if (equippedGun.reloading || equippedGun.cooldown > 0) return;
        if(gunList.Count > 1)
        {
            int index = gunIndex + modifier;
            if(index >= gunList.Count)
            {
                index = 0;
            } 
            else if (index < 0)
            {
                index = gunList.Count - 1;
            }
            gunIndex = index;
            Equip(gunList[index]);  
        }
    }
}
