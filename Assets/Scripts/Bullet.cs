using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;

    float speed = 3;
    Vector3 direction;

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    private void Start()
    {
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Her mermi clone olu�tu�unda PlayerController bulur
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
        speed += 1f;

        Collider[] targets = Physics.OverlapSphere(transform.position, 1);
        foreach (var item in targets)
        {
            if (item.tag is "Enemy")
            {
                PlayerController.destroyedEnemiesCount++; // D��man yok edildi�inde 1 artar
                PlayerController.DestroyedEnemiesText.text = "Destroyed Enemies: " + PlayerController.destroyedEnemiesCount.ToString() + "/3"; // Text ka� d��man yok edildi�ini g�sterir

                Destroy(gameObject); // Mermi yok edilir

                Destroy(item.gameObject); // D��man yok edilir               
            }

            if (item.tag is "Building") // E�er mermi yap�ya �arparsa
            {
                Destroy(gameObject); // Mermi yok edilir               
            }
        }
    }
}