using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment Instance;

    [Header("อุปกรณ์ที่ถืออยู่ในมือ")]
    public GameObject equippedFlashlight;
    public GameObject equippedKey;

    private bool flashlightOn = false;
    private bool keyShown = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ToggleFlashlight()
    {
        flashlightOn = !flashlightOn;
        if (equippedFlashlight != null)
            equippedFlashlight.SetActive(flashlightOn);
    }

    public void ToggleKey()
    {
        keyShown = !keyShown;
        if (equippedKey != null)
            equippedKey.SetActive(keyShown);
    }
}