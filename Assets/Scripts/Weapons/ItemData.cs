using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gun
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject modelPrefab;
    
}
