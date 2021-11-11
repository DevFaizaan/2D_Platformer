using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementFlying : MonoBehaviour
{

    public float enemySpeed;

    Animator enemyAnimator;

    //facing 
    public GameObject enemyGraphic;
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 5f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    float startChargeTime;
    bool isCharging;
    public bool chase = false;
    Rigidbody2D enemyRB;

    private GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 5) flipFacing();
            nextFlipChance = Time.time + flipTime;
        }
        if(Player == null)
        {
            return;
        }
        if (chase == true)
        {
            Chase();
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, enemySpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;
            isCharging = true;
            startChargeTime = Time.time + chargeTime;
        }
        

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (startChargeTime < Time.time)
            {
                Chase();

                enemyAnimator.SetBool("isCharging", isCharging);
            }
            chase = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            isCharging = false;
            enemyRB.velocity = new Vector2(0f, 0f);
            enemyAnimator.SetBool("isCharging", isCharging);
        }
        chase = false;
    }


    void flipFacing()
    {
        if (!canFlip) return;
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;

    }

}
