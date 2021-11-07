using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{

    public GameObject pickupEffect;
    public float healthAmount;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D Player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        Player.GetComponent<playerHealth>().addHealth(healthAmount);
        //playerHealth health = Player.GetComponent<playerHealth>().addHealth(multiplier);
  
        Destroy(gameObject);

    }

}
