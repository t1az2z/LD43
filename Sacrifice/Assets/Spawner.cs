using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float timeDelay;
    public int numberOfEnemiesToSpawn;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
	}
	
	private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i <numberOfEnemiesToSpawn; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemy.transform.SetParent(transform);
            enemy.transform.position = transform.position + new Vector3(Random.insideUnitCircle.x * 2,Random.insideUnitCircle.y * 2, 0);

        }
        yield return new WaitForSeconds(timeDelay);
    }
}
