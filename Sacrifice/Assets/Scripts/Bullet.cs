using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour {

    public Vector2 targetPos;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
	}

    private void OnCollisionEnter(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit");
        }
        else if (collision.CompareTag("Destructible"))
        {
            Debug.Log("Destructible Hit");
        }
    }
}
