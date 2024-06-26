using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum GunType
{
    SemiAutomatic,
    Automatic
}

public class Gun : MonoBehaviour
{
    private PlayerController owner;
    private PlayerWeaponHandler weaponHandler;
    public bool holding;
    public bool reloading;
    [SerializeField] private Transform bulletHole;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Gun Properties")]
    public GunModel model;
    public GunData gunData;
    public int magazineCur;
    public int ammo;
    public float cooldown;

    public void Initialize(PlayerController owner, PlayerWeaponHandler h_weapons)
    {
        this.owner = owner;
        weaponHandler = h_weapons;
        anim = this.SafeGetComponent(anim);
        if(magazineCur <= 0)
        {
            AnimSetBool("Empty", true);

        }
        else
        {
            AnimSetBool("Empty", false);

        }

        ammo = gunData.startAmmo;
        magazineCur = gunData.magazineSize;
    }

    private void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        
        if(holding && cooldown <= 0 && gunData.type == GunType.Automatic && !reloading)
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (reloading) return;
        if (magazineCur > 0)
        {
            if (cooldown <= 0)
            {
                cooldown = gunData.speed;
                magazineCur--;
                StartCoroutine(SpawnBullet());
                AnimSetTrigger("Fire");
                owner.h_hud.UpdateGunText();
                if(magazineCur <= 0)
                {
                    AnimSetBool("Empty", true);


                }
            }
        }
        else
        {
            weaponHandler.StopAimForReload();
        }
    }

    public void Reload()
    {
        if (magazineCur >= gunData.magazineSize) return;
        holding = false;
        if (magazineCur > 0)
            AnimSetTrigger("EmptyTrigger");
        for (int i = 0; i < gunData.magazineSize; i++) {
            if(magazineCur >= gunData.magazineSize)
            {
                gunData.magazineSize = magazineCur;
                break;
            }
            if (ammo <= 0) break;
            magazineCur++;
            ammo--;
        }
        AnimSetTrigger("Reload");
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator SpawnBullet()
    {
        Vector3 forward = owner.t_camera.forward;
        Vector3 origin = muzzle.position;
        float rayDistance = 1f;
        for(int i = 0; i < gunData.range; i++) {
            Debug.DrawRay(origin, forward, Color.red, 0.25f);
            Ray ray = new Ray(origin, forward);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, rayDistance)){
                print(hit.collider.gameObject);
                //Spawn the decal object just above the surface the raycast hit
                Transform decalObject = Instantiate(bulletHole, hit.point + (hit.normal * 0.025f), Quaternion.identity);
                //Rotate the decal object so that it's "up" direction is the surface the raycast hit
                decalObject.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal); ;
                Destroy(decalObject.gameObject, 2f);
                break;
            }
            Vector3 newOrigin = origin + (rayDistance * forward);
            origin = newOrigin;
            yield return new WaitForSeconds(1 / gunData.range);
        }
        yield return null;
    }

    IEnumerator ReloadCoroutine()
    {
        reloading = true;
        AnimSetBool("Empty", false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+1);
        owner.h_hud.UpdateGunText();
        reloading = false;
    }
    
    public void AnimSetBool(string name, bool value)
    {
        anim.SetBool(name, value);
        model.anim.SetBool(name, value);
    }

    public void AnimSetTrigger(string name)
    {
        anim.SetTrigger(name);
        model.anim.SetTrigger(name);
    }
}
