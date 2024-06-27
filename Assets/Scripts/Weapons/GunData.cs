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
    public byte damage;
    public byte range;
    public float speed;
    public ushort maxAmmo;
    public byte magazineSize;

    [Header("Other Settings")]
    public Vector3 aimOffset;
}
