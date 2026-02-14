using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public static int slain = 0;
    public float moveSpeed = 3f;
    private float baseSpeed = 3f;
    private bool manaBlastApplied = false;
    private bool inForceField = false;
    public static Transform playerTransform;
    private Vector2 diff;
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
            health = 100f;
        }
        if (PS.LuckyCharm && !PS.DoubleBullet)
        {
            health = 80f;
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
                TakeDamage(80f);
            }
            else if (!manaBlastApplied)
            {
                moveSpeed -= 1f;
                manaBlastApplied = true;
                StartCoroutine(DisableEffect(3f));
            }
        }
        float xDiff = playerTransform.position.x - transform.position.x;
        if (xDiff < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (xDiff > 0f)
        {
            spriteRenderer.flipX = false;
        }
        diff = transform.position - playerTransform.position;
        if (!PS.ForceFieldUpgrade)
        {
            bool inside = Mathf.Abs(diff.x) < 4f && Mathf.Abs(diff.y) < 4f;
            if (inside && !inForceField && PS.ForceField)
            {
                moveSpeed *= 0.5f;
                inForceField = true;
            }
            else if (!inside && inForceField)
            {
                inForceField = false;
                moveSpeed = baseSpeed;
            }
        }
        else if (PS.ForceFieldUpgrade)
        {
            bool inside = Mathf.Abs(diff.x) < 5f && Mathf.Abs(diff.y) < 5f;
            if (inside && !inForceField && PS.ForceField)
            {
                moveSpeed *= 0.5f;
                StartCoroutine(Burn(0.25f, 10f));
                inForceField = true;
            }
            else if (!inside && inForceField)
            {
                inForceField = false;
                StopCoroutine("Burn");
                moveSpeed = baseSpeed;
            }
        }
        if (moveSpeed < 0f)
        {
            moveSpeed = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageIndicator(damage);
        if (health <= 0)
        {
            slain += 1;
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
        GameObject droppedXP = Instantiate(droppedItemPrefab, transform.position , droppedItemPrefab.transform.rotation);
        Vector3 pos = droppedXP.transform.position;
        pos.x += Random.Range(-0.65f, 0.66f);
        pos.y += Random.Range(-0.4f, 0.5f);
        droppedXP.transform.position = pos;
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
        moveSpeed = baseSpeed;
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

    private IEnumerator Burn(float burnInterval, float burnDamage)
    {
        while (true)
        {
            TakeDamage(burnDamage);
            yield return new WaitForSeconds(burnInterval);
        }
    }
}
