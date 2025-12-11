using UnityEngine;

public class TopdownPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public float hpCurrent = 3f;
    public float iFrames = 2f;
    private float iFrameTimer = 0f;
    private bool invincible = false;
    private SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.15f; 
    private float blinkTimer = 0f;
    public PlayerStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        // Normalize movement
        if (movementInput.magnitude > 1)
        {
            movementInput.Normalize();
        }

        if (invincible)
        {
            iFrameTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkInterval)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; 
                blinkTimer = 0f;
            }

            if (iFrameTimer >= iFrames)
            {
                Debug.Log("Invincibility no longer active");
                iFrameTimer = 0f;
                blinkTimer = 0f;
                spriteRenderer.enabled = true;
                invincible = false;
            }
        }

        if (hpCurrent <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (invincible == false)
        {
            if (other.CompareTag("Enemy"))
            {
                if (playerStats.level >= 7 && playerStats.level < 10)
                {
                    var charmChance = Random.Range(1, 6);
                    if (charmChance == 3)
                    {
                        Destroy(other.gameObject);
                        Debug.Log("Charm activated! Enemy destroyed, no damage taken");
                        return;
                    }
                    hpCurrent -= 2f;
                    Debug.Log("Took damage, you are currently invincible");
                    invincible = true;
                    return;
                }
                if (playerStats.level >= 10)
                {
                    var charmChance = Random.Range(1, 4);
                    if (charmChance == 1)
                    {
                        Destroy(other.gameObject);
                        Debug.Log("Charm activated! Enemy destroyed, no damage taken");
                        return;
                    }
                    hpCurrent -= 2f;
                    Debug.Log("Took damage, you are currently invincible");
                    invincible = true;
                    return;
                }
                
                hpCurrent -= 1;
                Debug.Log("Took damage, you are currently invincible");
                invincible = true;
            }
        }
    }

    public void MoveSpeedIncrease()
    {
        moveSpeed += 1f;
    }

}
