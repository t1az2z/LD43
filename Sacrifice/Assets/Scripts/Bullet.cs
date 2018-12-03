using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : MonoBehaviour {

    public int damage;
    public bool destroyonCollision;
    Collider2D col;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public string ricSoundName; 

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            var hitSound = AudioManager.instance.FindClipByName("ZombieHit");
            hitSound.volume = Random.Range(.4f, .6f);
            hitSound.pitch = Random.Range(.95f, 1f);
            hitSound.Play();
        }
        else if (collider.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.instance.Play(ricSoundName);
        }
        else if (collider.gameObject.CompareTag("Destructible"))
        {
            Debug.Log("Destructible Hit");
        }

        if (destroyonCollision)
        {
            //Instantiate particles
            Destroy(gameObject, .02f);
        }
        else
        {
            col.enabled = false;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            transform.SetParent(collider.transform);
            if (collider.CompareTag("Enemy"))
            {
                sr.sortingLayerName = collider.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
                sr.sortingOrder -= 2;
            }
        }
    }
}
