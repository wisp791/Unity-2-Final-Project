using UnityEngine;
using System.Collections;

public class BossSlime : MonoBehaviour
{
    public float health = 500f;
    public float moveSpeed = 2f;
    private float baseSpeed = 2f;
    private Vector2 diff;
    private bool inForceField = false;
    private bool manaBlastApplied = false;
    public static Transform playerTransform;
    public SpriteRenderer spriteRenderer;
    public GameObject droppedItemPrefab;
    public static PlayerStats PS;
    public GameObject damageIndicatorPrefab;
    public EnemySpawner ES;
    public GameObject adsPrefab;
    public Transform hitPoint;

    void Start()
    {
        playerTransform = GameObject.FindFirstObjectByType<TopdownPlayerMovement>().transform;
        PS = GameObject.FindFirstObjectByType<PlayerStats>();
        ES = GameObject.FindFirstObjectByType<EnemySpawner>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BossAI());
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
                BossDamage(80f);
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
        bool inside = Mathf.Abs(diff.x) < 4f && Mathf.Abs(diff.y) < 4f;
        if (!PS.ForceFieldUpgrade)
        {
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
            if (inside && !inForceField && PS.ForceField)
            {
                moveSpeed *= 0.5f;
                StartCoroutine(Burn(0.5f, 10f));
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

    public void BossDamage(float damage)
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
            StopCoroutine("BossBlink");
            StartCoroutine(BossBlink(0.075f, 3f));
        }
    }

    public void Die()
    {
        int i = 0;
        while (i < 10)
        {
            GameObject droppedXP = Instantiate(droppedItemPrefab, transform.position , droppedItemPrefab.transform.rotation);
            droppedXP.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 255f);
            i++;
        }   
        Destroy(gameObject);
        ES.bossSpawned = false;
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

    private IEnumerator BossBlink(float blinkInterval, float blinkDuration)
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

    private IEnumerator BossAI()
    {
        while (true)
        {
            int action = Random.Range(1, 3);
            if (action == 1)
            {
                moveSpeed *= 2f;
                StartCoroutine(DisableEffect(1f));
            }
            if (action == 2)
            {
                GameObject spawnedAd = Instantiate(adsPrefab, GetRandomPositionFrom(transform.position), transform.rotation);
                GameObject spawnedAd2 = Instantiate(adsPrefab, GetRandomPositionFrom(transform.position), transform.rotation);
                GameObject spawnedAd3 = Instantiate(adsPrefab, GetRandomPositionFrom(transform.position), transform.rotation);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    
    Vector3 GetRandomPositionFrom(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x + Random.Range(-3f, 3f), position.y + Random.Range(-3f, 3f), position.z);
        return newPos;
    }
    
    private IEnumerator Burn(float burnInterval, float burnDamage)
    {
        while (true)
        {
            BossDamage(burnDamage);
            yield return new WaitForSeconds(burnInterval);
        }
    }
}
