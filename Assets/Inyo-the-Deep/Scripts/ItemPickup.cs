using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;           // ลาก ScriptableObject มาใส่ตรงนี้ใน Inspector
    public GameObject promptUI;     // ข้อความ "กด E เพื่อเก็บ"
    public GameObject pickupEffect; // เอฟเฟกต์ตอนเก็บ (Optional)

    private bool playerInRange = false;

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool success = InventoryManager.Instance.AddItem(item);

            if (success)
            {
                // ถ้าเป็นไฟฉาย ให้เปิดใช้งานทันที
                if (item.isFlashlight)
                {
                    PlayerFlashlight flashlight = FindAnyObjectByType<PlayerFlashlight>();
                    if (flashlight != null)
                        flashlight.GetFlashlight();
                }

                // เอฟเฟกต์ (ถ้ามี)
                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);

                if (promptUI != null)
                    promptUI.SetActive(false);

                Destroy(gameObject); // ลบไอเทมออกจากฉาก
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }
}