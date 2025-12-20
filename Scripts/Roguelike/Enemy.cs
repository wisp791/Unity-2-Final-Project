using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 3f;
    public Transform playerTransform;
    public GameObject droppedItemPrefab;
    public PlayerStats playerStats;
    public GameObject damageIndicatorPrefab;
    public Transform hitPoint;

    void Start()
    {
        playerTransform = GameObject.FindFirstObjectByType<TopdownPlayerMovement>().transform;
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;
        if (playerStats.DoubleBulletUpgrade)
        {
            moveSpeed = 4f;
        }
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageIndicator(damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(droppedItemPrefab, transform.position, droppedItemPrefab.transform.rotation);
        Destroy(gameObject);
    }

    public void DamageIndicator(float damage)
    {
        if (damageIndicatorPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            GameObject damageInstance = Instantiate(damageIndicatorPrefab, spawnPosition, Quaternion.identity);
            DmgNumber dn = damageInstance.GetComponent<DmgNumber>();
            if (dn != null)
            {
                dn.SetDamageText((int)damage);
            }
        }
    }
}
