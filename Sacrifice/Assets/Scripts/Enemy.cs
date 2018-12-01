using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int hp;
    public int damage;
    public int hpToRestore;
    public Collider2D collider;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        collider = GetComponent<Collider2D>();
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //Play sound and animation.
            player.AddHp(hpToRestore);
            collider.enabled = false;
        }
    }
}
