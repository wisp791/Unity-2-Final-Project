using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float interval = 2f;
    public GameObject player;
    public GameObject enemyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<TopdownPlayerMovement>().gameObject;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true) 
        { 
            GameObject spawnedEnemy = Instantiate(enemyPrefab, GetRandomPositionFrom(player.transform.position), enemyPrefab.transform.rotation);
            Vector2 diff = spawnedEnemy.transform.position - player.transform.position;
            if (Mathf.Abs(diff.x) < 2.25f && Mathf.Abs(diff.y) < 2.25f)
            {
                Destroy(spawnedEnemy);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    Vector3 GetRandomPositionFrom(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x + Random.Range(-10, 10), position.y + Random.Range(-10, 10), position.z);
        return newPos;
    }

    public void intervalDecrease(float decrease)
    {
        interval -= decrease;
        if (interval <= 0.1f)
        {
            interval = 0.1f;
        }
    }
}
