using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 0.5f; // ค่าเริ่มต้นปรับให้น้อยลง เพราะเราเอา deltaTime ออกแล้ว
    public Transform playerBody;

    public InputAction lookAction;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Rigidbody playerRb; // สร้างตัวแปรมารับ Rigidbody

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookAction.Enable();

        // ค้นหา Rigidbody จากตัวแคปซูล และจำมุมหันหน้าเริ่มต้นไว้
        if (playerBody != null)
        {
            playerRb = playerBody.GetComponent<Rigidbody>();
            yRotation = playerBody.eulerAngles.y;
        }
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        // จุดแก้ที่ 1: เอา Time.deltaTime ออก
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // หมุนกล้องก้ม/เงย (แกน X) ทำใน Update ได้เลยเพราะกล้องไม่มี Rigidbody
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // เก็บค่าการหมุนซ้าย/ขวา (แกน Y) เอาไว้
        yRotation += mouseX;
    }

    // จุดแก้ที่ 2: ใช้ FixedUpdate เพื่อให้ทำงานสอดคล้องกับระบบเดินของฟิสิกส์
    void FixedUpdate()
    {
        // สั่งหมุนตัวละครด้วย Rigidbody แก้ปัญหาภาพกระตุก 100%
        if (playerRb != null)
        {
            playerRb.MoveRotation(Quaternion.Euler(0f, yRotation, 0f));
        }
    }

    void OnDisable()
    {
        lookAction.Disable();
    }
}