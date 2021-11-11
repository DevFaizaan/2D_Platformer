using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<FlyingEnemy>().Chase();
            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            FlyingEnemy enemyScript = collision.GetComponent<FlyingEnemy>();
            enemyScript.chase = false;

        }
    }
}
