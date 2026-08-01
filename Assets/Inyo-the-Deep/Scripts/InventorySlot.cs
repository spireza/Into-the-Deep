using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image iconImage;
    public Image highlightImage; 
    public int slotIndex;

    private InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
        if (highlightImage != null)
            highlightImage.enabled = false;
    }

  
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryUI == null) return;

        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryUI.OnSlotLeftClick(slotIndex);
        }
        
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryUI.OnSlotRightClick(slotIndex);
        }
    }

    
    public void SetHighlight(bool active)
    {
        if (highlightImage != null)
            highlightImage.enabled = active;
    }
}