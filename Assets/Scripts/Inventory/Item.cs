using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
   public string itemName;
   public Sprite itemIcon;
   public string itemDescription;
   public int count = 1;
}
