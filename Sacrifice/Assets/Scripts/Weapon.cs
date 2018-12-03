using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class Weapon : MonoBehaviour {

    public GameObject bulletSpawner;
    public WeaponType weaponType;
    SpriteRenderer sr;
    WeaponType currentWeapon;
    public float knockback;
    Bullet bullet;
    float rateOfFire;
    public float dispersion;
    [HideInInspector]public int shootCost;
    string shootSound;
    string ricochetSound;
    Player player;
    CinemachineImpulseSource impulse;
    public GameObject shell;

    

	// Use this for initialization

	void Start ()
    {
        InitializeWeapon(weaponType);
        player = transform.parent.GetComponent<Player>();
        impulse = GetComponent<CinemachineImpulseSource>();
    }


    public IEnumerator Shoot(Vector2 direction)
    {
        player.allowFire = false;
        impulse.GenerateImpulse(new Vector3(knockback/2, knockback/2, 0));
        Instantiate(shell, transform.position, Quaternion.identity);
        
        var currentBullet = Instantiate(bullet, bulletSpawner.transform.position, transform.rotation);
        currentBullet.knockback = knockback;
        var bSr = currentBullet.gameObject.GetComponent<SpriteRenderer>();
        bSr.sprite = currentWeapon.bulletSprite;
        if (currentWeapon.destroyProjectileOnCollision)
            bSr.sortingOrder += 1;
        var rb = currentBullet.GetComponent<Rigidbody2D>();
        direction = new Vector2(direction.x + Random.Range(-dispersion, dispersion), direction.y + Random.Range(-dispersion, dispersion)).normalized;
        rb.velocity = direction * currentWeapon.bulletSpeed;
        currentBullet.damage = currentWeapon.damage;
        currentBullet.destroyonCollision = currentWeapon.destroyProjectileOnCollision;
        currentBullet.ricSoundName = ricochetSound;
        var shotSound = AudioManager.instance.FindClipByName(shootSound);
        shotSound.volume = Random.Range(.8f, 1f);
        shotSound.pitch = Random.Range(.95f, 1f);
        shotSound.Play();
        yield return new WaitForSeconds(rateOfFire);
        player.allowFire = true;

    }

    public void InitializeWeapon(WeaponType wpn)
    {
        sr = GetComponent<SpriteRenderer>();
        currentWeapon = wpn;
        shootCost = wpn.shootCost;
        sr.sprite = wpn.sprite;
        knockback = wpn.knockback;
        bullet = wpn.bulletType.GetComponent<Bullet>();
        shootSound = wpn.shootSoundName;
        ricochetSound = wpn.ricochetSoundName;
        rateOfFire = wpn.rateOfFire;
        dispersion = wpn.dispersion;

    }
}
