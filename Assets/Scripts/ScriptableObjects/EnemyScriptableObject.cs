using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptableObject", order = 3)]
public class EnemyScriptableObject : ScriptableObject
{
    public string enemyName;
    public int baseHp;
    public float statScalingMultiplier;
    public int damage;
    public float minSpeed;
    public float maxSpeed;
    public float minMovementDelay;
    public float maxMovementDelay;
    public float minAttackDelay;
    public float maxAttackDelay;
    public float spread;
    public float bulletCount;
    public float bulletSpeed;
    public float difficultyPoint;
    public GameObject bulletPrefab;
}
