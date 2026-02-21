using UnityEngine;
using UnityEngine.Events;

public class CollectibleXp : MonoBehaviour
{
    public int xpAmount = 10;
    private float despawnTimer = 15f;
    private float timer = 0f;
    private PlayerStats PS;
    private Vector2 distance;
    private Transform playerTransform;
    public float pickupSpeed = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PS = GameObject.FindFirstObjectByType<PlayerStats>().GetComponent<PlayerStats>();
        playerTransform = GameObject.FindFirstObjectByType<TopdownPlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = transform.position - playerTransform.position;
        timer += Time.deltaTime;
        if (timer >= despawnTimer)
        {
            Destroy(gameObject);
        }
        if (Mathf.Abs(distance.x) < PS.pickUpRadius && Mathf.Abs(distance.y) < PS.pickUpRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, pickupSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
