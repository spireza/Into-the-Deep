using UnityEngine;
using UnityEngine.SceneManagement; // เพิ่มบรรทัดนี้บนสุด

public class LockedDoor : MonoBehaviour
{
    [Header("กุญแจที่ต้องการ")]
    public ItemData requiredKey;

    [Header("ข้อความ")]
    public GameObject lockedPrompt;
    public GameObject unlockPrompt;

    [Header("ประตู (แบบ Animator)")]
    public Animator doorAnimator;
    public bool destroyKeyOnUse = true;

    [Header("ประตู (แบบหมุน Pivot)")]
    public Transform doorPivot;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("เปลี่ยนซีน")]
    public string nextSceneName = "Room_1";      // ใส่ชื่อ Scene ถัดไป
    public float delayBeforeLoad = 1.5f; // รอให้ประตูหมุนเปิดก่อนค่อยเปลี่ยนซีน

    private bool playerInRange = false;
    private bool isUnlocked = false;
    private bool sceneLoadTriggered = false;
    private Quaternion closedRotation, openRotation;

    void Start()
    {
        if (lockedPrompt != null) lockedPrompt.SetActive(false);
        if (unlockPrompt != null) unlockPrompt.SetActive(false);

        if (doorPivot != null)
        {
            closedRotation = doorPivot.localRotation;
            openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    void Update()
    {
        if (isUnlocked && doorPivot != null)
            doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, openRotation, Time.deltaTime * openSpeed);

        if (!playerInRange || isUnlocked)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryUnlock();
        }
    }

    void TryUnlock()
    {
        if (InventoryManager.Instance.HasItem(requiredKey))
        {
            isUnlocked = true;

            if (destroyKeyOnUse)
                InventoryManager.Instance.RemoveItemByName(requiredKey.itemName);

            if (doorAnimator != null)
                doorAnimator.SetTrigger("Open");
            else if (doorPivot == null)
                Debug.Log("ประตูเปิดแล้ว!");

            if (lockedPrompt != null) lockedPrompt.SetActive(false);
            if (unlockPrompt != null) unlockPrompt.SetActive(false);

            // เริ่มนับเวลาถอยหลังเพื่อเปลี่ยนซีน
            if (!string.IsNullOrEmpty(nextSceneName) && !sceneLoadTriggered)
            {
                sceneLoadTriggered = true;
                Invoke(nameof(LoadNextScene), delayBeforeLoad);
            }
        }
        else
        {
            Debug.Log("ต้องการ " + requiredKey.itemName + " เพื่อเปิดประตูนี้");
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isUnlocked)
            {
                bool hasKey = InventoryManager.Instance.HasItem(requiredKey);
                if (hasKey && unlockPrompt != null) unlockPrompt.SetActive(true);
                else if (!hasKey && lockedPrompt != null) lockedPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (lockedPrompt != null) lockedPrompt.SetActive(false);
            if (unlockPrompt != null) unlockPrompt.SetActive(false);
        }
    }
}