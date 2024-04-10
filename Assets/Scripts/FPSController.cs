using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public float jumpHeight;
    public float gravity = -9.81f; // Terrain üzerindeki fiziksel olaylar için yerçekimi

    private CharacterController characterController;
    public Camera mainCamera;
    private float rotationX;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // WASD veya yön tuþlarý ile hareket input alýnýr
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal; // Hareket yönünü belirler
        
        characterController.Move(moveDirection.normalized * speed * Time.deltaTime); // Hareket hýzla çarpýlýr

        velocity.y += gravity * Time.deltaTime; // Karakterin havada kalmamasý için uygun yerçekimi kuvveti uygulanýr
        characterController.Move(velocity * Time.deltaTime); // Karakter yerçekimi kuvvetine göre hareket eder (yokuþ inme, yokuþ çýkma vb.)

        // Fareye göre kamera rotasyonu ayarlanýr
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Kamera 360 dönmemesi için en yukarýda ve en aþaðýda olacak þekilde sýnýrlandýrýlýr

        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
