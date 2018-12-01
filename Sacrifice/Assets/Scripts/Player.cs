using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;
    private Vector2 moveVelocity;
    Vector2 moveInput;
    [HideInInspector]public float externalForcesFactor = 1;
    private Vector2 mousePosition;

    public Weapon weapon;


    public int hp = 10;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if (Input.GetMouseButtonDown(0))
            weapon.Shoot();
        RotateAndFlipDependingOnMousePos(mousePosition);
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        PlayerMovement();
    }

    void RotateAndFlipDependingOnMousePos(Vector3 mousePos)
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weapon.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        if (weapon.transform.eulerAngles.z > 90 && weapon.transform.eulerAngles.z < 270)
        {
            weapon.GetComponentInChildren<SpriteRenderer>().flipY = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            weapon.GetComponentInChildren<SpriteRenderer>().flipY = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void PlayerMovement()
    {
        moveVelocity = moveInput.normalized * speed * externalForcesFactor;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(collision);
        }
    }

    private void TakeDamage(Collision2D collision)
    {
        hp -= collision.gameObject.GetComponent<Enemy>().damage;
        print(hp);
        //animation play take damage
        //sound play takedamage
    }
}
