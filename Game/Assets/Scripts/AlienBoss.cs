using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{ 
    //idle stage
    public float idleMoveSpeed;
    public Vector2 idleMoveDirection;

//up and down attack stage
public float attackMoveSpeed;
public Vector2 attackMoveDirection;

//player attack stage
public float attackPlayerSpeed;
public Transform player;

//other
public Transform groundCheckUp;
public Transform groundCheckDown;
public Transform groundCheckWall;
public float groundCheckRadius;
public LayerMask groundLayer;
private bool isTouchingUp;
private bool isTouchingDown;
private bool isTouchingWall;
    private bool isGoingUp = true;
    private bool facingLeft = true;

    private Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
        //idleState();
        attackMovement();
    }

    void idleState()
    {
        if(isTouchingUp && isGoingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !isGoingUp)
        {
            ChangeDirection();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        enemyRB.velocity = idleMoveSpeed * idleMoveDirection;
    }

    void attackMovement()
    {
        if (isTouchingUp && isGoingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !isGoingUp)
        {
            ChangeDirection();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        enemyRB.velocity = attackMoveSpeed * attackMoveDirection;
    }

    void ChangeDirection()
    {
        isGoingUp = !isGoingUp;
            idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    void onDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }
}
