using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyStats;
    public int hp = 3;
    private float nextShootTime;
    private PlayerStats player;

    [Header("--- Set up manually ---")]
    public List<GameObject> powerups;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dieFX;
    public bool canBeCleanedUp = true;

    [Header("--- Read only ---")]
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
    public Room roomAssociated;
    public bool dead = false;
    public GameObject bulletPrefab;

    public EnemyMovementController enemyMovementController;

    // Start is called before the first frame update
    public virtual void Start()
    {
        ReferenceSetUp();
        StatsSetUp();
        SetUpMovementController();
    }

    //public IEnumerator Spawn()
    //{
    //    yield return null;
    //}

    public void ReferenceSetUp()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        player = PlayerStats.Instance;
    }

    public virtual void StatsSetUp()
    {
        statScalingMultiplier = enemyStats.statScalingMultiplier;
        hp = enemyStats.baseHp;
        int scaledHp = (int)(Mathf.Pow(statScalingMultiplier, roomAssociated.roomIndex) * (float)hp);
        if (hp < scaledHp)
        {
            hp = scaledHp;
        }
        baseHp = enemyStats.baseHp;
        damage = enemyStats.damage;
        minSpeed = enemyStats.minSpeed;
        maxSpeed = enemyStats.maxSpeed;
        minMovementDelay = enemyStats.minMovementDelay;
        maxMovementDelay = enemyStats.maxMovementDelay;
        minAttackDelay = enemyStats.minAttackDelay;
        maxAttackDelay = enemyStats.maxAttackDelay;
        spread = enemyStats.spread;
        bulletCount = enemyStats.bulletCount;
        bulletSpeed = enemyStats.bulletSpeed;
        difficultyPoint = enemyStats.difficultyPoint;
        bulletPrefab = enemyStats.bulletPrefab;
        nextShootTime = Time.fixedTime + Random.Range(minAttackDelay, maxAttackDelay);
    }

    public void SetUpMovementController()
    {
        enemyMovementController = GetComponent<EnemyMovementController>();
        enemyMovementController.minSpeed = minSpeed;
        enemyMovementController.maxSpeed = maxSpeed;
        enemyMovementController.minMovementDelay = minMovementDelay;
        enemyMovementController.maxMovementDelay = maxMovementDelay;
    }

    public void SetAssociatedRoom(Room room)
    {
        roomAssociated = room;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.fixedTime >= nextShootTime && !dead)
        {
            nextShootTime = Time.fixedTime + Random.Range(minAttackDelay, maxAttackDelay);
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet shotBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                shotBullet.damage = damage;
                shotBullet.speed = bulletSpeed;
                shotBullet.Shoot(Quaternion.Euler(0, 0, Random.Range(spread, -spread)) 
                    * (player.gameObject.transform.position - transform.position));
            }
        }
    }

    public void TakeDamage(int amount)
    {
        GameState curState = GameManager.Instance.currentGameState;
        if (!dead && curState == GameState.Battling)
        {
            hp -= amount;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    public void Die(bool spawnPowerup = true)
    {
        dead = true;
        enemyMovementController.StopMoving();
        animator.SetTrigger("die");
        //die sfx
        if (dieFX != null)
            Instantiate(dieFX, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemySpriteFlip>().enabled = false;

        if (powerups.Count > 0 && spawnPowerup)
        {
            Instantiate(powerups[Random.Range(0, powerups.Count)], transform.position, Quaternion.identity, transform);
        }
        if (roomAssociated != null)
        {
            roomAssociated.EnemyKilled(this);
        }
    }
}
