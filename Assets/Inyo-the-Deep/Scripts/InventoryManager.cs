using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public int slotCount = 6;
    public List<ItemData> items = new List<ItemData>();

    public event Action OnInventoryChanged;

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
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItemByName(string itemName)
    {
        ItemData found = items.Find(i => i.itemName == itemName);
        if (found != null)
        {
            items.Remove(found);
            OnInventoryChanged?.Invoke();
        }
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count) return;
        ItemData item = items[index];

        if (item.isConsumable)
        {
            Debug.Log("ใช้ " + item.itemName + " ฮีล " + item.healAmount);
            items.RemoveAt(index);
            OnInventoryChanged?.Invoke();
        }
        else if (item.isFlashlight)
        {
            if (PlayerEquipment.Instance != null)
                PlayerEquipment.Instance.ToggleFlashlight();
        }
        else if (item.isKey)
        {
            if (PlayerEquipment.Instance != null)
                PlayerEquipment.Instance.ToggleKey();
        }
        else
        {
            Debug.Log("เลือกไอเทม: " + item.itemName);
        }
    }

    public void DropItem(int index, Vector3 dropPosition)
    {
        if (index < 0 || index >= items.Count) return;
        items.RemoveAt(index);
        OnInventoryChanged?.Invoke();
        // TODO: ถ้าอยากให้ของไปโผล่ในฉากจริง ค่อย Instantiate prefab ตรงนี้ทีหลัง
    }
}