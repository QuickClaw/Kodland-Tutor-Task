using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rifleStart;
    [SerializeField] private Text HpText;
    public Text DestroyedEnemiesText;

    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Victory;

    public float health;

    public int destroyedEnemiesCount;

    private Camera mainCamera;

    void Start()
    {
        ChangeHealth(100); // Oyuncunun can� 100 ile ba�lar
        mainCamera = GameObject.Find("Player").GetComponent<FPSController>().mainCamera; // Sahne ba��nda oyuncunun kameras� bulunur
    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        else if (health <= 0)
        {
            Lost();
        }
        HpText.text = health.ToString();
    }

    public void Win()
    {
        Victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        GameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(mainCamera.transform.forward); // Mermi oyuncunun bakt��� y�ne do�ru ate�lenir
            buf.transform.rotation = mainCamera.transform.rotation; // Merminin rotation de�eri kameran�n rotation de�eri ile ayn� yap�l�r
        }

        if (Input.GetMouseButtonDown(1))
        {
            Collider[] tar = Physics.OverlapSphere(transform.position, 2);
            foreach (var item in tar)
            {
                if (item.tag is "Enemy")
                {
                    Destroy(item.gameObject);
                }
            }
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets)
        {
            if (item.tag is "Heal")
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            if (item.tag is "Finish" && destroyedEnemiesCount == 3) // �� d��man yok edilmeden oyun kazan�lamaz
            {
                Win();
            }
            if (item.tag is "Enemy")
            {
                Lost();
            }
        }
    }
}
