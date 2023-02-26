using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    public int damage = 0;
    public bool destroyable = true;
    public GameObject destroyFX;
    public LayerEnum damageLayer;
    private BulletManager bulletManager;
    private SFXManager soundFXManager;

    private void OnEnable()
    {
        bulletManager = BulletManager.Instance;
        bulletManager.bullets.Add(gameObject);
        soundFXManager = SFXManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)damageLayer)
        {
            if (damageLayer == LayerEnum.Enemy)
            {
                soundFXManager.PlayHitSFX();
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            else
            {
                soundFXManager.PlayHurtSFX();
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
        if (destroyable)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (destroyFX != null)
            Instantiate(destroyFX, transform.position, Quaternion.identity);

        bulletManager.bullets.Remove(gameObject);
    }
}
