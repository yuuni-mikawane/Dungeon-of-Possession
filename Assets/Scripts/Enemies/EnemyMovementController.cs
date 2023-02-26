using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Header("Changable Movement Values")]
    public float minSpeed;
    public float maxSpeed;
    public bool hasDynamicMovementRange = true;
    public float minRange = 1f;
    public float maxRange = 10f;
    public float minMovementDelay; //in seconds
    public float maxMovementDelay; //in seconds
    private float nextMoveTime;
    public float maxMovementDuration = 1.5f;
    private float endOfMovementTime;

    [Header("Movement Status")]
    public bool isMoving;
    public Vector3 nextWaypoint;
    private Vector3 movementRangeCenter;
    private Rigidbody2D rb;
    public float currentMovementSpeed;
    public bool movable = true;

    private void Start()
    {
        nextMoveTime = Time.time;
        movementRangeCenter = transform.position;
        rb = GetComponent<Rigidbody2D>();
        GenerateNextMove();
    }
    private void FixedUpdate()
    {
        if (Time.time >= nextMoveTime && movable)
            Move();
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
        movable = false;
    }

    private void GenerateNextMove()
    {
        //get new position
        if (hasDynamicMovementRange)
            movementRangeCenter = transform.position;
        nextWaypoint = Random.onUnitSphere * Random.Range(minRange, maxRange) + movementRangeCenter;
        isMoving = false;
        nextMoveTime = Time.time + Random.Range(minMovementDelay, maxMovementDelay);
        endOfMovementTime = nextMoveTime + maxMovementDuration;
        currentMovementSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Move()
    {
        isMoving = true;
        rb.velocity = (Vector2)(nextWaypoint - transform.position).normalized * currentMovementSpeed;

        //stop movement
        if (Vector2.Distance(nextWaypoint, transform.position) < 0.1f ||
            Time.time >= endOfMovementTime)
        {
            rb.velocity = Vector2.zero;
            GenerateNextMove();
        }

    }

    private void OnDrawGizmosSelected()
    {
        //max moverange
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);

        //min moverange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRange);

        //next waypoint
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(nextWaypoint, 0.1f);
    }
}
