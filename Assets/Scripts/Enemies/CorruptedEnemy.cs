using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedEnemy : Enemy
{
    public CopiedStats copiedStats;
    public int currentHp = 0;

    public override void Start()
    {
        ReferenceSetUp();
        StatsSetUp();
        powerups[0].GetComponent<Powerup>().compoundStats = copiedStats;
        SetUpMovementController();
    }

    public override void StatsSetUp()
    {
        float atkDelayMult = GameManager.Instance.currentDifficulty.corruptedEnemyAttackDelayScale;
        statScalingMultiplier = 1.1f;
        baseHp = (int)(Mathf.Pow(statScalingMultiplier, roomAssociated.roomIndex) * 50f);
        Debug.Log(baseHp);
        Debug.Log(currentHp);

        if (currentHp != 0)
            hp = currentHp;
        else
            hp = baseHp;

        Debug.Log(hp);

        damage = copiedStats.damage;
        minSpeed = copiedStats.speed/3;
        maxSpeed = copiedStats.speed;
        minMovementDelay = 0f;
        maxMovementDelay = 0f;
        minAttackDelay = copiedStats.attackDelay * atkDelayMult;
        maxAttackDelay = copiedStats.attackDelay * atkDelayMult * 2f;
        spread = 30f;
        bulletCount = copiedStats.bulletCount;
        bulletSpeed = copiedStats.bulletSpeed/3;
    }
}
