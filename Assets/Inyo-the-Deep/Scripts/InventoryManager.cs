using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public int slotCount = 6;
    public List<ItemData> items = new List<ItemData>();
    public InventoryUI inventoryUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool AddItem(ItemData newItem)
    {
        if (items.Count >= slotCount)
        {
            Debug.Log("ช่องเก็บของเต็ม!");
            return false;
        }
        items.Add(newItem);
        if (inventoryUI != null) inventoryUI.UpdateUI(items);
        return true;
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
        if (inventoryUI != null) inventoryUI.UpdateUI(items);
    }
}