using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerType
{
    Weapon,
    Ui,
    UIObject,
    WeaponModel
}

public class LayerAdjust : MonoBehaviour
{
    public LayerType type;
    public PlayerCameraContainer playerCamera;
    public PlayerController playerController;
    private int mask;

    private void Start()
    {
        if(type == LayerType.Weapon)
        {
            playerCamera = this.SafeGetComponentInParent(playerCamera);
            playerController = playerCamera.controller;
        }
        else
        {
            playerController = this.SafeGetComponentInParent(playerController);
        }


        switch (type)
        {
            case LayerType.Weapon:
                mask = playerController.weaponLayer; break;
            case LayerType.UIObject:
                mask = playerController.uioLayer; break;
            case LayerType.WeaponModel:
                mask = playerController.modelWeaponLayer; break;
            default:
                break;
        }
        gameObject.layer = mask;
        foreach(Transform go in gameObject.GetComponentsInChildren<Transform>())
        {
            go.gameObject.layer = mask;
        }
    }

}
