using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("ข้อมูลพื้นฐาน")]
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;

    [Header("ประเภทไอเทม")]
    public bool isKey;          
    public bool isFlashlight;   
    public bool isConsumable;   

    [Header("เอฟเฟกต์ (ถ้าเป็น Consumable)")]
    public int healAmount = 0;  
}