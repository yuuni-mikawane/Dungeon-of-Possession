using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    private float horizontalDirection;
    private float verticalDirection;

    private PlayerStats playerStats;
    [SerializeField] private Animator animator;
    private CircleCollider2D circleCollider;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = PlayerStats.Instance;
        circleCollider = GetComponent<CircleCollider2D>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementDirection();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void UpdateMovementDirection()
    {
        horizontalDirection = GetInput().x;
        verticalDirection = GetInput().y;
        if (horizontalDirection == 0 && verticalDirection == 0)
            animator.SetBool("moving", false);
        else
            animator.SetBool("moving", true);
    }

    private void MoveCharacter()
    {
        if (gameManager.currentGameState != GameState.Unconcious &&
            gameManager.currentGameState != GameState.Killed)
            rb.velocity = GetInput().normalized * playerStats.speed;
        else
            DisableColliderPhysics();
    }

    public void DisableColliderPhysics()
    {
        circleCollider.enabled = false;
        rb.velocity = Vector2.zero;
    }

    public void EnableColliderPhysics()
    {
        circleCollider.enabled = true;
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
