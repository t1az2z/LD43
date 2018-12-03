using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject gates;
    public float timeDelay;
    public int numberOfEnemiesToSpawn = 8;
    public int numberOfWaves = 8;
    float spawnRadius;
    bool isSpawning = false;
    bool finished = false;
    public bool onGates = false;
    
	void Start () {
        spawnRadius = GetComponent<CircleCollider2D>().radius;
    }
    private void Update()
    {
        if (finished && onGates)
        {
            bool open = false;
            foreach (Transform child in transform)
            {
                open = !child.transform.GetChild(0).GetComponent<Enemy>().alive;
            }

            if (open)
                if (gates != null)
                {
                    gates.GetComponent<Animator>().SetTrigger("Open");
                }
        }
    }
    private IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        for (int i = 0; i < numberOfWaves; i++)
        {
            for (int x = 0; x < numberOfEnemiesToSpawn; x++)
            {
                var enemy = Instantiate(enemyPrefab);
                enemy.transform.SetParent(transform);
                enemy.transform.position = transform.position + new Vector3(Random.insideUnitCircle.x * spawnRadius, Random.insideUnitCircle.y * spawnRadius, 0);
            }
            yield return new WaitForSeconds(timeDelay);
        }
        finished = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (!isSpawning)
                StartCoroutine(SpawnEnemy());
    }
}
