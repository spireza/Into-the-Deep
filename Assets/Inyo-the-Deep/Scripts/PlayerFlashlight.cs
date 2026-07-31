using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public GameObject flashlightObject;
    public Light flashlightLight;

    private bool hasFlashlight = false;
    private bool isOn = false;

    void Start()
    {
        // ตอนเริ่มเกม ยังไม่มีไฟฉาย
        flashlightObject.SetActive(false);
    }

    void Update()
    {
        // ถ้ามีไฟฉายแล้ว กด F เพื่อเปิด-ปิด
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;

            flashlightLight.enabled = isOn;
        }
    }

    public void GetFlashlight()
    {
        hasFlashlight = true;

        // แสดงโมเดลไฟฉาย
        flashlightObject.SetActive(true);

        // ตอนเพิ่งเก็บมา ให้ไฟยังดับก่อน
        isOn = false;
        flashlightLight.enabled = false;

        Debug.Log("ได้รับไฟฉายแล้ว");
    }
}