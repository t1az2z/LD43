using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons")]
public class WeaponType : ScriptableObject {

    public Sprite sprite;
    public float knockback;

    public Bullet bulletType;
    public float rateOfFire;
}
