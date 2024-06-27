using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    public delegate void AimPosMethod();


    private GunModel curModel;

    [Header("Guns Variables")]
    [SerializeField] private GunData startGun;
    public Gun equippedGun;
    public byte maxGuns = 2;
    public List<Gun> gunList;

    private sbyte gunIndex;

    private bool aiming;

    [Header("Parents")]
    [SerializeField] private Transform gunContainer;
    [SerializeField] private Transform gunModelContainer;
    [SerializeField] private Transform aimPosition;
    private Vector3 targetPos;
    private Vector3 startPos;
    
    [Header("Components")]
    [SerializeField] private Pickupable pickupPrefab;
    public PlayerController controller;

    private void Start()
    {
        startPos = gunContainer.localPosition;
        targetPos = aimPosition.localPosition;
        PickupGun(startGun);
    }

    private void Update()
    {
        if (equippedGun != null)
        {
            if (aiming)
            {
                targetPos = aimPosition.localPosition + equippedGun.gunData.aimOffset;
            }
            else
            {
                targetPos = startPos;
            }
        }

        if(gunContainer.position != targetPos)
        {
            gunContainer.localPosition = Vector3.MoveTowards(gunContainer.localPosition, targetPos, 2 *Time.deltaTime);
        }
    }

    public bool MaxAmmoCheck(Gun gun)
    {
        if (gun.ammo >= gun.gunData.maxAmmo) return true;
        return false;
    }

    public void AddAmmo(int amount)
    {
        //Add ammo to equipped gun
        //If equipped gun has max ammo, top off each gun until ammo off
      

        equippedGun.ammo += (ushort)amount;
        if(equippedGun.ammo >= equippedGun.gunData.maxAmmo)
        {
            equippedGun.ammo = equippedGun.gunData.maxAmmo;
            controller.h_hud.UpdateGunText();
        }
    }

    public void PickupGun(GunData gun)
    {
        if (gunList.Count >= maxGuns)
        {
            //if max amount of guns
            gunList.Remove(equippedGun);
            //Spawn pickup object
            Pickupable pu = Instantiate(pickupPrefab, gunContainer.position, Quaternion.identity);
            //initialize pickup object
            pu.Initialize(equippedGun.gunData);
            Destroy(equippedGun.gameObject);
        }
        //Create the new gun instance and its model
        Gun newGun = Instantiate(gun.gunPrefab, gunContainer);
        GunModel model = Instantiate(gun.objectPrefab, gunModelContainer);
        newGun.model = model;
        gunList.Add(newGun);
        gunIndex = (sbyte)(gunList.Count - 1);
        Equip(newGun);
    }
    public void Equip(Gun gun) {
        if(equippedGun != null && curModel != null)
        {
            //Turns gun off
            equippedGun.gameObject.SetActive(false);
            curModel.gameObject.SetActive(false);
        }
        //sets equipped gun
        equippedGun = gun;
        curModel = gun.model;
        
        //turns gun on
        curModel.gameObject.SetActive(true);
        equippedGun.gameObject.SetActive(true);

        equippedGun.Initialize(controller, this);
        controller.h_hud.UpdateGunText();
    }
    public void CycleWeapon(sbyte modifier = 1)
    {
        //As long as gun is reloading you cant cycle weapons
        if (equippedGun.reloading || equippedGun.cooldown > 0 || aiming) return;
        if (gunList.Count > 1)
        {
            sbyte index = (sbyte)(gunIndex + modifier);
            if (index >= gunList.Count)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = (sbyte)(gunList.Count - 1);
            }
            gunIndex = index;
            Equip(gunList[index]);
        }
    }
    public void Aim(bool toggle)
    {
        //Ease weapon container into aiming position
        //Add weapon aim offset to aiming position
        if (equippedGun.reloading) return;
        aiming = toggle;
    }
    public void OnAimInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Aim(true);
        }
        if (context.canceled)
        {
            Aim(false);
        }
        
    }
    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (equippedGun == null) return;
        if (equippedGun.gunData.type == GunType.Automatic)
        {
            if (context.performed)
            {
                //mouse is holding
                equippedGun.holding = true;
            }
            if (context.canceled)
            {
                //mouse is no longer holding
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
            if (equippedGun.magazineCur == equippedGun.gunData.magazineSize) return;
            StopAimForReload();
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
    
    public void StopAimForReload()
    {
        if (!aiming)
        {
            equippedGun.Reload();
        }
        else
        {
            aiming = false;
            StartCoroutine(WaitUntilAimFinished(startPos, equippedGun.Reload));
        }
    }

    public IEnumerator WaitUntilAimFinished(Vector3 endPos, AimPosMethod aimMethod)
    {
        yield return new WaitUntil(() => gunContainer.localPosition == endPos);
        aimMethod();
        
    }

}
