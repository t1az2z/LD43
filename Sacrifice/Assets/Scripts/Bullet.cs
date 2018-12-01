using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;
    public bool destroyonCollision;
    Collider2D col;
    Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if (collider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit");
        }
        else if (collider.gameObject.CompareTag("Destructible"))
        {
            Debug.Log("Destructible Hit");
        }

        if (destroyonCollision)
            Destroy(gameObject, 3);
        else
        {
            col.enabled = false;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            transform.SetParent(collider.transform);
        }
    }
}
