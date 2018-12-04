using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

    public float speed;

    float bleedTimePeriod = 5f;
    Rigidbody2D rb;
    private Vector2 moveVelocity;
    Vector2 moveInput;
    [HideInInspector]public float externalForcesFactor = 1;
    private Vector3 mousePosition;

    public Weapon weapon;
    public int shootCost;
    public int ammo;
    public int maxAmmo;
    public int ammoAmountToGetForPrice = 10;
    Vector3 oldPos;

    public int maxHP = 10;
    public int hp = 10;
    [Tooltip("In HP")] public int ammoPrice = 1;
    SpriteRenderer sr;
    public bool isDead = false;

    public bool allowFire;

    public Animator animator;

    public int ammoFromPickupWeapon = 3;

    CinemachineImpulseSource impulse;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        maxAmmo = weapon.maxAmmo;
        if (ammo > maxAmmo)
            ammo = maxAmmo;
        oldPos = weapon.transform.localPosition;
        allowFire = true;
        impulse = GetComponent<CinemachineImpulseSource>();
        StartCoroutine(Bleed(bleedTimePeriod));
    }

    private void Update()
    {

        shootCost = weapon.shootCost;
        maxAmmo = weapon.maxAmmo;
        if (ammo > maxAmmo)
            ammo = maxAmmo;

        if (!isDead)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (moveInput != Vector2.zero)
            {
                animator.SetBool("isWalking", true);
                if (!AudioManager.instance.IsPlaying("PlayerWalk"))
                    AudioManager.instance.Play("PlayerWalk");
            }
            else
            {
                animator.SetBool("isWalking", false);
                AudioManager.instance.Play("PlayerWalk");
            }

            mousePosition = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                if (ammo - shootCost >= 0 && allowFire)
                {
                    StartCoroutine(weapon.Shoot(mousePosition.normalized));
                    StartCoroutine(Knockback(mousePosition, weapon.knockback, .15f));
                    ammo -= shootCost;
                }
                else if (ammo - shootCost < 0)
                {
                    AudioManager.instance.Play("OutOfAmmo");
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //play knife audio
                animator.SetTrigger("KnifeHit");
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
            AudioManager.instance.Play("PlayerDeath");
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
        if (ammo + ammoAmountToGetForPrice <= maxAmmo)
        {
            TakeDamage(ammoPrice);
            ammo += ammoAmountToGetForPrice;
        }
        else
        {
            TakeDamage(ammoPrice);
            ammo += maxAmmo - ammo;
        }
        animator.SetTrigger("getAmmo");
        impulse.GenerateImpulse();
        AudioManager.instance.Play("GetAmmo");
  
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
            animator.SetTrigger("Damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp") && !isDead)
        {
            GameManager.Instance.ui.textAnim.SetBool("ShowTooltip", true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.ui.textAnim.SetBool("ShowTooltip", false);
                weapon.InitializeWeapon(collision.GetComponent<WeaponPickUp>().weapon);
                shootCost = weapon.shootCost;
                maxAmmo = weapon.maxAmmo;
                if (ammo > maxAmmo)
                    ammo = maxAmmo;
                if (ammo + ammoFromPickupWeapon <= maxAmmo)
                    ammo += ammoFromPickupWeapon;
                else
                    ammo += maxAmmo - ammoFromPickupWeapon;
                AudioManager.instance.Play("WeaponPickUp");
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp") && !isDead)
        {
            //show tooltip
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.ui.textAnim.SetBool("ShowTooltip", false);
                weapon.InitializeWeapon(collision.GetComponent<WeaponPickUp>().weapon);
                if (ammo > maxAmmo)
                    ammo = maxAmmo;
                if (ammo + ammoFromPickupWeapon <= maxAmmo)
                    ammo += ammoFromPickupWeapon;
                else
                    ammo += maxAmmo - ammoFromPickupWeapon;
                AudioManager.instance.Play("WeaponPickUp");
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            GameManager.Instance.ui.textAnim.SetBool("ShowTooltip", false);
        }
    }
    public void AddHp(int ammount)
    {
        GameManager.Instance.ui.heal.Play("UITakeDamage");
        if (!isDead)
        {
            if (hp + ammount <= maxHP)
                hp += ammount;
            else
                hp += maxHP - hp;
        }
    }

    private void TakeDamage(Collision2D collision)
    {
        var damage = collision.transform.parent.gameObject.GetComponent<Enemy>().damage;
        AudioManager.instance.Play("PlayerDamage");
        impulse.GenerateImpulse();
        GameManager.Instance.StartCoroutine(GameManager.Instance.FreezeTime(.05f));
        if (hp - damage > 0)
            hp -= damage;
        else
            hp = 0;

        //animation play take damage
        //sound play takedamage
        //start coroutine invulnerable
    }

    private void TakeDamage(int damage)
    {
        AudioManager.instance.Play("PlayerDamage");
        impulse.GenerateImpulse();
        GameManager.Instance.StartCoroutine(GameManager.Instance.FreezeTime(.05f));
        GameManager.Instance.ui.damage.Play("UITakeDamage");
        if (hp - damage > 0)
            hp -= damage;
        else
            hp = 0;
    }

    private IEnumerator Bleed(float time)
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(time);
            TakeDamage(1);
        }
    }
}
