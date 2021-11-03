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
    bool aboveEntity = true;
    float flipTime = 5f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    float startChargeTime;
    bool isCharging;
    Rigidbody2D enemyRB;


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
                if (!facingRight && aboveEntity) enemyRB.AddForce(new Vector2(-1, 1) * enemySpeed);
                else enemyRB.AddForce(new Vector2(1, -1) * enemySpeed);

                enemyAnimator.SetBool("isCharging", isCharging);
            }

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
