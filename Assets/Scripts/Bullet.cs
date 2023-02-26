using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageHitbox
{
    public float speed = 5f;

    public void Shoot(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
