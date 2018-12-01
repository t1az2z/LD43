using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullets")]
public class Bullet : ScriptableObject {


    public Sprite sprite;
    public Collider2D collider;

    public float speed;
    public int damage;

}
