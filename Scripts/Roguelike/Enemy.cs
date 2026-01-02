using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 3f;
    private bool manaBlastApplied = false;
    public static Transform playerTransform;
    public SpriteRenderer spriteRenderer;
    public GameObject droppedItemPrefab;
    public static PlayerStats PS;
    public GameObject damageIndicatorPrefab;
    public Transform hitPoint;

    void Start()
    {
        playerTransform = GameObject.FindFirstObjectByType<TopdownPlayerMovement>().transform;
        PS = GameObject.FindFirstObjectByType<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (PS.DoubleBullet)
        {
            health = 120f;
            moveSpeed = 3.2f;
        }
        if (PS.LuckyCharm)
        {
            health = 40f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        if (PS.manaBlastActive)
        {
            Vector2 diff = transform.position - playerTransform.position;
            float blastRadius = 4.5f;
            if (diff.magnitude * diff.magnitude <= blastRadius * blastRadius)
            {
                Destroy(this.gameObject);
            }
            else if (!manaBlastApplied)
            {
                moveSpeed -= 1f;
                manaBlastApplied = true;
                StartCoroutine(DisableEffect(3f));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageIndicator(damage);
        if (health <= 0)
        {
            Die();
        }
        if (PS.manaBulletActive)
        {
            moveSpeed -= 1f;
            StartCoroutine(DisableEffect(3f));
            StopCoroutine("SlowBlink");
            StartCoroutine(SlowBlink(0.075f, 3f));
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
                dn.SetDamageText((float)damage);
            }
        }
    }

    private IEnumerator DisableEffect(float effectLength)
    {
        yield return new WaitForSeconds(effectLength);
        moveSpeed += 1f;
        manaBlastApplied = false;
    }

    private IEnumerator SlowBlink(float blinkInterval, float blinkDuration)
    {
        float timer = 0f;
        while (timer <= blinkDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval * 2;
        }
    }
}
