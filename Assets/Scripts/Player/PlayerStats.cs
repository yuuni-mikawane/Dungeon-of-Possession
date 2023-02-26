using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class PlayerStats : SingletonBind<PlayerStats>
{
    public CharacterScriptableObject character;
    public int maxHp;
    public float maxSanity;
    public int hp;
    public float sanity;
    public int damage;
    public float speed;
    public bool stunned = false;
    public float cooldownTime;
    public float skillValue;
    public float attackDelay = 1f;
    public float spread;
    public int bulletCount;
    public float bulletSpeed = 20f;

    private bool concious = true;
    private GameManager gameManager;
    private LevelManager levelManager;
    public MovementController movementController;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dieFX;
    [SerializeField] private GameObject compoundPowerup;
    [SerializeField] private GameObject deadHat;
    [SerializeField] private Transform deadHatPos;
    [SerializeField] private bool spawnHat = false;
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
        levelManager = LevelManager.Instance;
        movementController = GetComponent<MovementController>();
        SetUpStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameState == GameState.Battling)
        {
            isInvincible = false;
            if (sanity > 0 && concious)
            {
                sanity -= Time.deltaTime;
            }
            else
            {
                concious = false;
                sanity = 0;
                Die();
                gameManager.currentGameState = GameState.Unconcious;
            }
        }
        else
        {
            isInvincible = true;
        }
    }

    private void SetUpStats()
    {
        if (character != null)
        {
            maxHp = character.baseHp;
            maxSanity = character.baseSanity;
            damage = character.baseDamage;
            speed = character.baseSpeed;
            cooldownTime = character.baseCooldown;
            skillValue = character.baseSkillValue;
            attackDelay = character.baseAttackDelay;
            spread = character.baseSpread;
            bulletCount = character.baseBulletCount;
            bulletSpeed = character.baseBulletSpeed;
        }
        hp = maxHp;
        sanity = maxSanity;
        concious = true;
    }

    public void Respawn()
    {
        if (spawnHat)
            Instantiate(deadHat, deadHatPos.position, Quaternion.identity);
        SetUpStats();
        movementController.enabled = true;
        movementController.EnableColliderPhysics();
        GetComponent<ShootController>().enabled = true;
        GetComponent<PlayerSpriteFlip>().enabled = true;
        animator.SetTrigger("respawn");
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            hp -= amount;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    private void Die()
    {
        isInvincible = true;
        CopiedStats statCopy = new CopiedStats(damage, speed, attackDelay, bulletCount, bulletSpeed);
        //if room is not yet corrupted
        if (!levelManager.currentRoom.isCorrupted)
        {
            levelManager.currentRoom.isCorrupted = true;
            gameManager.currentGameState = GameState.Unconcious;
            levelManager.currentRoom.RecordCurrentStats(statCopy);
            animator.SetTrigger("sleep");
            gameManager.OnPlayerDie("FAINTED");
        }
        //if killed by past wizard
        else
        {
            gameManager.currentGameState = GameState.Killed;
            animator.SetTrigger("die");
            Instantiate(dieFX, transform.position, Quaternion.identity);
            Instantiate(compoundPowerup, transform.position, Quaternion.identity).GetComponent<Powerup>().compoundStats = statCopy;

            gameManager.OnPlayerDie("DESTROYED");
            spawnHat = true;
        }
        SFXManager.Instance.PlayDieSFX();
        movementController.enabled = false;
        movementController.DisableColliderPhysics();
        GetComponent<ShootController>().enabled = false;
        GetComponent<PlayerSpriteFlip>().enabled = false;
    }
}
