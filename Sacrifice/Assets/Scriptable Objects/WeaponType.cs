using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons")]
public class WeaponType : ScriptableObject {

    public Sprite sprite;
    public float knockback;

    public GameObject bulletType;
    public float rateOfFire;

    public Sprite bulletSprite;
    public int shootCost;

    public float bulletSpeed;
    public int damage;
    public ParticleSystem bulletParticles;

    public bool destroyProjectileOnCollision = true;

    public float dispersion;
    public Sound sootSound;

}
