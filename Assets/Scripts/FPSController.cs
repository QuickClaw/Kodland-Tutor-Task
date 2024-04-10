using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public float jumpHeight;
    public float gravity = -9.81f; // Terrain �zerindeki fiziksel olaylar i�in yer�ekimi

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
        // WASD veya y�n tu�lar� ile hareket input al�n�r
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal; // Hareket y�n�n� belirler
        
        characterController.Move(moveDirection.normalized * speed * Time.deltaTime); // Hareket h�zla �arp�l�r

        velocity.y += gravity * Time.deltaTime; // Karakterin havada kalmamas� i�in uygun yer�ekimi kuvveti uygulan�r
        characterController.Move(velocity * Time.deltaTime); // Karakter yer�ekimi kuvvetine g�re hareket eder (yoku� inme, yoku� ��kma vb.)

        // Fareye g�re kamera rotasyonu ayarlan�r
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Kamera 360 d�nmemesi i�in en yukar�da ve en a�a��da olacak �ekilde s�n�rland�r�l�r

        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
