using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    [SerializeField] int hp;
    public int damage;
    public int hpToRestore;
    public Collider2D collider;
    public Player player;
    public float speed;
    public Vector2[] path;
    Pathfinder pf;
    float step;
    Vector2 oldPlayerPos;
    Vector2 currentPlayerPos;
    bool playerMoved;
    public GameObject walls;
    int current = 0;
    
    bool alive = true;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        collider = GetComponent<Collider2D>();
        pf = new Pathfinder(AINavMeshGenerator.instance);
        step = speed * Time.deltaTime;
        currentPlayerPos = player.transform.position;
    }

    private void Update()
    {    
        if (player != null && alive)
        {
            path = pf.FindPath(transform.position, player.transform.position, walls);
            if (path != null)
            {
                if (path.Length > 0)
                {
                    if (current >= path.Length)
                        current = 0;
                    if (Vector2.Distance(path[current], transform.position) < .1f)
                    {
                        current++;
                    }
                    if (current+1 <= path.Length)
                    {
                        
                        transform.position = Vector2.MoveTowards(transform.position, path[current], Time.deltaTime * speed);
                    }
                    else
                        current = 0;
                }

                /*for (var i = 1; i < path.Length; i++)
                {
                    Debug.DrawLine(path[i - 1], path[i]);
                }*/
            }            
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
        alive = false;
        //Play sound and animation.
        player.AddHp(hpToRestore);
        collider.enabled = false;
    }


}
