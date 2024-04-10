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
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Her mermi clone oluþtuðunda PlayerController bulur
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
                PlayerController.destroyedEnemiesCount++; // Düþman yok edildiðinde 1 artar
                PlayerController.DestroyedEnemiesText.text = "Destroyed Enemies: " + PlayerController.destroyedEnemiesCount.ToString() + "/3"; // Text kaç düþman yok edildiðini gösterir

                Destroy(gameObject); // Mermi yok edilir

                Destroy(item.gameObject); // Düþman yok edilir               
            }

            if (item.tag is "Building") // Eðer mermi yapýya çarparsa
            {
                Destroy(gameObject); // Mermi yok edilir               
            }
        }
    }
}