using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    public GameObject promptUI;

    private bool playerInRange = false;

    void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.AddItem(item);

            // ถ้าไอเทมนี้เป็นไฟฉาย ให้สั่งเปิดใช้งานทันที
            if (item.isFlashlight)
            {
                PlayerFlashlight flashlight = FindAnyObjectByType<PlayerFlashlight>();
                if (flashlight != null)
                {
                    flashlight.GetFlashlight();
                }
            }

            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptUI != null) promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null) promptUI.SetActive(false);
        }
    }
}