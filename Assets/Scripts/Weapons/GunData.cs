using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ItemData
{
    public Gun gunPrefab;
    public GunModel objectPrefab;
    [Header("Gun Properties")]
    public GunType type;
    public string gunName;
    public int damage;
    public float range;
    public float speed;
    public int startAmmo;
    public int magazineSize;
}
