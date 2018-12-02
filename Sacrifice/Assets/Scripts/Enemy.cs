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
    public AIDestinationSetter AIdesset;
    public AILerp ailerp;
    bool alive = true;
    public SpriteRenderer sr;
    public float agroRadius;
    Animator animator;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        collider = GetComponent<Collider2D>();
        AIdesset = transform.parent.GetComponent<AIDestinationSetter>();
        AIdesset.target = player.transform;
        ailerp = transform.parent.GetComponent<AILerp>();
        ailerp.speed = speed;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        ailerp.enabled = false;
    }

    private void Update()
    {

         if (Physics2D.OverlapCircle(transform.position, agroRadius,11))
        {
            animator.SetTrigger("Agro");
        }

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
        animator.SetTrigger("Dead");
        /*alive = false;
        AIdesset.enabled = false;
        ailerp.enabled = false;
        //Play sound and animation.
        player.AddHp(hpToRestore);
        collider.enabled = false;*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            animator.SetTrigger("Attack");
    }


}
