using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] slotIcons;

    public void UpdateUI(List<ItemData> items)
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            if (i < items.Count)
            {
                slotIcons[i].sprite = items[i].icon;
                var c = slotIcons[i].color;
                c.a = 1f;
                slotIcons[i].color = c;
            }
            else
            {
                slotIcons[i].sprite = null;
                var c = slotIcons[i].color;
                c.a = 0f;
                slotIcons[i].color = c;
            }
        }
    }
}