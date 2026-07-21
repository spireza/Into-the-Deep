using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;     // ตัว Player
    public float speed = 3f;     // ความเร็วในการเดิน
    public float stopDistance = 1.5f; // ระยะที่จะหยุด

    void Update()
    {
        if (player == null)
            return;

        // คำนวณระยะห่าง
        float distance = Vector3.Distance(transform.position, player.position);

        // ถ้ายังไม่ถึงระยะหยุด ให้เดินเข้าหา
        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }

        // หันหน้าเข้าหา Player
        transform.LookAt(player);
    }
}