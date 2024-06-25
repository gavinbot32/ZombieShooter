using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Transform t_camera;
    public PlayerMovement h_movement;
    public PlayerLook h_look;
    public PlayerHUD h_hud; 
    public Health h_health;
    public PlayerWeaponHandler h_weapon;
    public InteractHandler h_interact;

    [Header("Cameras")]
    public Camera baseCamera;
    public Camera uiCamera;
    public Camera uioCamera;
    public Camera weaponCamera;

    public LayerMask uioMask;
    public LayerMask weaponMask;
    public LayerMask baseMask;

    public int uioLayer;
    public int weaponLayer;
    public int modelWeaponLayer;

    private void Awake()
    {
        SetComponents();
    }

    private void Start()
    {
        SetOtherComponents();
    }
    private void SetComponents()
    {
        h_movement = this.SafeGetComponent(h_movement);
        h_look = this.SafeGetComponent(h_look);
        h_hud = this.SafeGetComponent(h_hud);
        h_health = this.SafeGetComponent(h_health);
        h_weapon = this.SafeGetComponent(h_weapon);
        h_interact = this.SafeGetComponent(h_interact);
    }

    private void SetOtherComponents()
    {
        SetCameraLayer(uioCamera, uioMask);
        SetCameraLayer(weaponCamera, weaponMask);
    }

    public void SetCameraLayer(Camera cam, LayerMask layer)
    {
        cam.cullingMask = layer;
    }


}
