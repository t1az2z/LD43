using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

    public WeaponType[] weapons;
    public WeaponType weapon;
    SpriteRenderer sr;
    private Collider2D col;


    private void Start()
    {
        weapon = weapons[Random.Range(0, weapons.Length)];
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = weapon.sprite;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, 2);
        foreach(var c in cols)
        {
            if (c.gameObject.CompareTag("PickUp") && c.GetComponent<Collider2D>() != col)
                Destroy(c.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            print("destr");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            print("destr");
            Destroy(gameObject);
        }
    }


}
