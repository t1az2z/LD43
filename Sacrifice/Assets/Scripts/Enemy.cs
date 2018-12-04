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
    public bool alive = true;
    public SpriteRenderer sr;
    public float agroRadius;
    Animator animator;
    public LayerMask playerLayer;
    public AudioSource steps;
    public AudioSource death;
    float rnd;
    bool agro = false;
    public GameObject weaponToSpawn;
    public int weaponDropChance;
    bool weaponSpawned = false;

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
        rnd = Random.Range(0, 100);
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, agroRadius, playerLayer) && !player.isDead && !agro)
        {
            animator.SetTrigger("Agro");
            agro = true;
        }

    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("Damage");
        if (hp <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        animator.SetTrigger("Dead");
        if (!weaponSpawned)
        {
            if (rnd >= 100 - weaponDropChance)
                Instantiate(weaponToSpawn, transform.position, Quaternion.identity);
            weaponSpawned = true;
        }
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
        {
            animator.SetTrigger("Attack");
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<Player>().isDead)
            animator.SetTrigger("Attack");
    }*/


}
