using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("ช่องเก็บของ")]
    public InventorySlot[] slots;  

    [Header("หน้าต่าง Inventory")]
    public GameObject inventoryPanel;
    public KeyCode toggleKey = KeyCode.Tab;

    [Header("รายละเอียดไอเทม")]
    public GameObject tooltipPanel;
    public Text tooltipNameText;
    public Text tooltipDescText;

    [Header("ปุ่มทิ้งของ")]
    public Button dropButton;      

    private int selectedSlot = -1;
    private bool isOpen = false;

    void Start()
    {
        
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);

        
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged += UpdateUI;

        if (dropButton != null)
            dropButton.onClick.AddListener(OnDropButtonClicked);

        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }

        // เช็คปุ่ม 1-6 เพื่อใช้ไอเทมในช่องนั้นๆ
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) UseSlot(5);
    }

    void UseSlot(int index)
    {
        List<ItemData> items = InventoryManager.Instance.items;

        if (index >= items.Count)
        {
            Debug.Log("ช่องนี้ว่างอยู่");
            return;
        }

        SelectSlot(index);
        ShowTooltip(items[index]);
        InventoryManager.Instance.UseItem(index);
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(isOpen);

            
            Time.timeScale = isOpen ? 0f : 1f;
        }

      
        if (!isOpen && tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }

   
    public void UpdateUI()
    {
        List<ItemData> items = InventoryManager.Instance.items;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
               
                slots[i].iconImage.sprite = items[i].icon;
                var c = slots[i].iconImage.color;
                c.a = 1f;
                slots[i].iconImage.color = c;
            }
            else
            {
               
                slots[i].iconImage.sprite = null;
                var c = slots[i].iconImage.color;
                c.a = 0.1f; 
                slots[i].iconImage.color = c;
                slots[i].SetHighlight(false);
            }
        }
    }

 
    public void OnSlotLeftClick(int index)
    {
        List<ItemData> items = InventoryManager.Instance.items;

        if (index >= items.Count) return;

       
        SelectSlot(index);

        
        ShowTooltip(items[index]);

       
        InventoryManager.Instance.UseItem(index);
    }

    
    public void OnSlotRightClick(int index)
    {
        List<ItemData> items = InventoryManager.Instance.items;

        if (index >= items.Count) return;

       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dropPos = player ? player.transform.position + player.transform.forward * 1.5f : Vector3.zero;

        InventoryManager.Instance.DropItem(index, dropPos);

       
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);

        selectedSlot = -1;
    }

   
    public void OnDropButtonClicked()
    {
        if (selectedSlot >= 0)
        {
            OnSlotRightClick(selectedSlot);
        }
    }

    void SelectSlot(int index)
    {
    
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetHighlight(false);

       
        selectedSlot = index;
        if (index < slots.Length)
            slots[index].SetHighlight(true);
    }

    void ShowTooltip(ItemData item)
    {
        if (tooltipPanel == null) return;

        tooltipPanel.SetActive(true);

        if (tooltipNameText != null)
            tooltipNameText.text = item.itemName;

        if (tooltipDescText != null)
            tooltipDescText.text = item.description;
    }
}