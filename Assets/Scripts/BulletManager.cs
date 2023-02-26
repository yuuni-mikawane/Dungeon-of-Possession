using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class BulletManager : SingletonBind<BulletManager>
{
    public List<GameObject> bullets;

    public void ClearBullets(float delay)
    {
        StartCoroutine(DestroyAllBullets(delay));
    }

    IEnumerator DestroyAllBullets(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
        bullets.Clear();
    }
}
