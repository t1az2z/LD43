using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour {

    [SerializeField] int hp;
    public int damage;
    public int hpToRestore;
    public Collider2D collider;
    public Player player;
    public float speed;
    int current = 0;
    AIDestinationSetter AIdesset;
    AILerp ailerp;
    bool alive = true;
    private SpriteRenderer sr;
    


    private void Start()
    {
        player = FindObjectOfType<Player>();
        collider = GetComponent<Collider2D>();
        AIdesset = transform.parent.GetComponent<AIDestinationSetter>();
        AIdesset.target = player.transform;
        ailerp = transform.parent.GetComponent<AILerp>();
        ailerp.speed = speed;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.transform.position.x > transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        alive = false;
        AIdesset.enabled = false;
        ailerp.enabled = false;
        //Play sound and animation.
        player.AddHp(hpToRestore);
        collider.enabled = false;
    }


}
