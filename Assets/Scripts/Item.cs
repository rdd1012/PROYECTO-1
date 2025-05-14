using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] 
public class Item : ScriptableObject {
    public int itemID;
    public string itemName;
    public Sprite icon;
    public int usos = 1;
    public bool isReadable=false;
    public string carta=null;
    public bool givesItem = false;
    public List<Item> itemsQueDa;
}