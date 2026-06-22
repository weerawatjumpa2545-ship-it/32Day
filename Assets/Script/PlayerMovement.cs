using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementV6 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction.Enable();
    }

    void FixedUpdate()
    {
        // รับค่าจากคีย์บอร์ด (W, A, S, D)
        Vector2 inputVector = moveAction.ReadValue<Vector2>();

        // จุดที่แก้ไข: เปลี่ยนมาใช้ transform.right (ซ้าย/ขวา) และ transform.forward (หน้า/หลัง)
        // เพื่อให้ตัวละครเดินไปตามทิศทางที่มันกำลังหันหน้าอยู่จริงๆ
        Vector3 movement = (transform.right * inputVector.x) + (transform.forward * inputVector.y);

        // ปกป้องไม่ให้ความเร็วพุ่งเกินเวลาเดินแนวทแยง (กด W+A พร้อมกัน)
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // สั่งให้ Rigidbody เคลื่อนที่
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnDisable()
    {
        moveAction.Disable();
    }
}