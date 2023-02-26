using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingPoint;
    public Transform hand;

    private Vector2 lookDir;
    private float lookAngle;

    private PlayerStats playerStats;
    private float nextShootTime;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        PointBarrel();
    }

    private void FixedUpdate()
    {
        DetectShoot();
    }

    private void PointBarrel()
    {
        lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90);
    }

    private void DetectShoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && nextShootTime <= Time.time)
        {
            for(int i = 0; i < playerStats.bulletCount; i++)
            {
                Bullet shotBullet = Instantiate(bullet, shootingPoint.position, Quaternion.identity).GetComponent<Bullet>();
                shotBullet.damage = playerStats.damage;
                shotBullet.speed = playerStats.bulletSpeed;
                shotBullet.Shoot(Quaternion.Euler(0, 0, Random.Range(playerStats.spread, -playerStats.spread)) * hand.transform.up);
            }
            nextShootTime = Time.fixedTime + playerStats.attackDelay;
        }
    }
}
