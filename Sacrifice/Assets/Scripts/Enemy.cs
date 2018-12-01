using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int hp;
    public int damage;
    Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //Play sound and animation.
            //Player.AddHp
            collider.enabled = false;
        }
    }
}
