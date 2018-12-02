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

        if (player != null)
        {
            path = pf.FindPath(transform.position, player.transform.position, walls);
            if (path != null)
            {
                for (var i = 1; i < path.Length; i++)
                {
                    Debug.DrawLine(path[i - 1], path[i]);
                }
                foreach (var point in path)
                {
                    if (Vector2.Distance(transform.position, point) > .01f)
                        transform.position = Vector2.MoveTowards(transform.position, point, step);
                    else
                        continue;
                }
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
        //Play sound and animation.
        player.AddHp(hpToRestore);
        collider.enabled = false;
    }


}
