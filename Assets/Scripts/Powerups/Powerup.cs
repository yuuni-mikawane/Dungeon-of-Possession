using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Powerup : MonoBehaviour
{
    public PowerupEnum powerupType;
    public float value;
    private bool pickedUp = false;
    public CopiedStats compoundStats;
    private PlayerStats playerStats;
    private SFXManager sfxManager;
    private CircleCollider2D col;

    private void Start()
    {
        sfxManager = SFXManager.Instance;
        playerStats = PlayerStats.Instance;
        col = GetComponent<CircleCollider2D>();
        transform.DOMoveY(transform.position.y + 0.3f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerEnum.Player)
        {
            if (!pickedUp)
            {
                pickedUp = true;
                switch (powerupType)
                {
                    case PowerupEnum.Damage:
                        playerStats.damage += (int)value;
                        break;
                    case PowerupEnum.FireSpeed:
                        playerStats.attackDelay *= value;
                        break;
                    case PowerupEnum.Hp:
                        playerStats.Heal((int)value);
                        break;
                    case PowerupEnum.BulletCount:
                        playerStats.bulletCount += (int)value;
                        break;
                    case PowerupEnum.SkillCooldown:
                        playerStats.cooldownTime *= value;
                        break;
                    case PowerupEnum.MovementSpeed:
                        playerStats.speed += value;
                        break;
                    case PowerupEnum.BulletSpeed:
                        playerStats.bulletSpeed += value;
                        break;
                    case PowerupEnum.Compound:
                        if (compoundStats != null)
                        {
                            playerStats.damage += Mathf.Abs(playerStats.character.baseDamage - compoundStats.damage);
                            if (playerStats.attackDelay > compoundStats.attackDelay)
                                playerStats.attackDelay = compoundStats.attackDelay;
                            playerStats.Heal(100);
                            playerStats.bulletCount += Mathf.Abs(playerStats.character.baseBulletCount - compoundStats.bulletCount);
                            playerStats.speed += Mathf.Abs(playerStats.character.baseSpeed - compoundStats.speed);
                            playerStats.bulletSpeed += Mathf.Abs(playerStats.character.baseBulletSpeed - compoundStats.bulletSpeed);
                        }
                        break;
                }
                //powerup pick up fx
                col.enabled = false;
                sfxManager.PlaySwallowSFX();
                transform.DOKill();
                transform.DOMoveY(transform.position.y + 2f, 0.5f).SetEase(Ease.InQuart);
                GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).OnComplete(() => Destroy(gameObject));
            }
        }
    }
}
