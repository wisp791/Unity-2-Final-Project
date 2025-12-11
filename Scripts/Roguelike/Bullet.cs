using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 1f;
    public float speed = 10f;
    private static bool sizeUpgrade = false;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!sizeUpgrade)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        else
        {
            transform.localScale = new Vector3(4f, 4f, 4f);
        }
        if (rb != null)
        {
            rb.velocity = transform.right * speed;
        }
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // if no rb
        if (rb == null)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    public void DamageIncrease()
    {
        damage += 5f;
    }

    public void sizeIncrease()
    {
        sizeUpgrade = true;
    }
}
