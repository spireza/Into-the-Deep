using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [Header("เสียงเท้า")]
    public AudioClip[] footstepClips;   // ลากไฟล์เสียงหลายอันใส่ได้
    public float stepInterval = 0.5f;    // ระยะเวลาระหว่างก้าว (วินาที)
    public float minMoveSpeed = 0.1f;    // ความเร็วขั้นต่ำถึงจะเริ่มเล่นเสียง

    private AudioSource audioSource;
    private CharacterController controller;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // เช็คว่าผู้เล่นกำลังเดินอยู่บนพื้นและมีความเร็วพอ
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (controller.isGrounded && horizontalVelocity.magnitude > minMoveSpeed)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // รีเซ็ตให้เล่นเสียงทันทีตอนเริ่มเดินใหม่
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0 || audioSource == null) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}