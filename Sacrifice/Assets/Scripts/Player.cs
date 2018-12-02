using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;
    private Vector2 moveVelocity;
    Vector2 moveInput;
    [HideInInspector]public float externalForcesFactor = 1;
    private Vector3 mousePosition;

    public Weapon weapon;
    private int shootCost;
    public int ammo;
    public int maxAmmo;
    public int ammoAmountToGetForPrice = 10;
    Vector3 oldPos;

    public int maxHP = 10;
    public int hp = 10;
    [Tooltip("In HP")] public int ammoPrice = 1;
    SpriteRenderer sr;
    public bool isDead = false;

    public Animator animator;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        shootCost = weapon.shootCost;
        oldPos = weapon.transform.localPosition;
    }

    private void Update()
    {
        if (!isDead)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (moveInput != Vector2.zero)
                animator.SetBool("isWalking", true);
            else
                animator.SetBool("isWalking", false);
            mousePosition = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                if (ammo - shootCost >= 0)
                {
                    weapon.Shoot(mousePosition.normalized);
                    StartCoroutine(Knockback(mousePosition, weapon.knockback, .15f));
                    ammo -= shootCost;
                }
            }
            RotateAndFlipDependingOnMousePos(mousePosition);
        }
        else
        {
            moveInput = Vector2.zero;
            mousePosition = Vector2.zero;
        }


        if (Input.GetButtonDown("Jump"))
        {
            GetAmmo();
        }





        if (hp <= 0)
        {
            rb.isKinematic = true;
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    private void LateUpdate()
    {
        
    }

    void FixedUpdate ()
    {
        PlayerMovement();
    }

    private IEnumerator Knockback(Vector3 direction, float strength, float duration)
    {
        weapon.transform.localPosition -= direction * .01f* strength;
        yield return new WaitForSeconds(duration);
        weapon.transform.localPosition = oldPos;
    }

    void GetAmmo()
    {
        if (ammo < maxAmmo)
        {
            animator.SetTrigger("getAmmo");
            if (ammo + ammoAmountToGetForPrice <= maxAmmo)
            {
                hp -= ammoPrice;
                ammo += ammoAmountToGetForPrice;
            }
            else
            {
                hp -= ammoPrice;
                ammo += maxAmmo - ammo;
            }
        }
        else
        {
            //indicate max ammo
        }
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
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        rb.velocity = moveVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(collision);
        }
    }

    public void AddHp(int ammount)
    {
        if (hp + ammount <= maxHP)
            hp += ammount;
        else
            hp += maxHP - hp;
    }

    private void TakeDamage(Collision2D collision)
    {
        hp -= collision.gameObject.GetComponent<Enemy>().damage;

        //animation play take damage
        //sound play takedamage
        //start coroutine invulnerable
    }


}
