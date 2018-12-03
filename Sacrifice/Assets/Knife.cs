using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Knife : MonoBehaviour {

    public int damage;
    public CinemachineImpulseSource impulse;
    public float knockback;


    private void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !GameManager.Instance.player.isDead)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            impulse.GenerateImpulse(new Vector3(knockback * .8f, knockback * .8f, 0));
            GameManager.Instance.StartCoroutine(GameManager.Instance.FreezeTime(.02f));
            var hitSound = AudioManager.instance.FindClipByName("ZombieHit");
            hitSound.volume = UnityEngine.Random.Range(.4f, .6f);
            hitSound.pitch = UnityEngine.Random.Range(.95f, 1f);
            hitSound.Play();
        }
    }
}
