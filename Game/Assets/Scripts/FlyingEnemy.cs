using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject enemyGraphic;
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 5f;
    float nextFlipChance = 0f;

    public float enemySpeed;
    public bool chase = false;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 5) flipFacing();
            nextFlipChance = Time.time + flipTime;
        }

        if (Player == null)
            return;
        if(chase == true)
        {
            Chase();
        }
        
        flipFacing();
    }

    public void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, enemySpeed * Time.deltaTime);
    }

   // private void returnToStart()
  //  {
   //     transform.position = Vector2.MoveTowards(transform.position, startPoint.position, enemySpeed * Time.deltaTime);
   // }


    void flipFacing()
    {
        if (!canFlip) return;
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            chase = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            chase = false;

        }
    }
}
