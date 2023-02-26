using System;
using UnityEngine;

[Serializable]
public class CopiedStats
{
    public int damage;
    public float speed;
    public float attackDelay;
    public int bulletCount;
    public float bulletSpeed;

    public CopiedStats(int damage, float speed, float attackDelay, int bulletCount, float bulletSpeed)
    {
        this.damage = damage;
        this.speed = speed;
        this.attackDelay = attackDelay;
        this.bulletCount = bulletCount;
        this.bulletSpeed = bulletSpeed;
    }
}
