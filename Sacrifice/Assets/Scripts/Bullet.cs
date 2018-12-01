using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject, 3);
        }
        else if (collider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit");
            Destroy(gameObject, 3);
        }
        else if (collider.gameObject.CompareTag("Destructible"))
        {
            Debug.Log("Destructible Hit");
            Destroy(gameObject, 3);
        }
    }
}
