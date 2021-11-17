using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{

    public float enemyMaxHealth;

    public GameObject enemyDeathFX;
    public Slider enemySlider;

    float currentHealth;
    public GameObject barrier;

    //enemy sound
    public AudioClip enemyHurt;
    AudioSource enemyAS;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;

        enemyAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDamage(float damage)
    {
        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage;
        enemySlider.value = currentHealth;

        enemyAS.clip = enemyHurt;
        enemyAS.Play();
        enemyAS.PlayOneShot(enemyHurt);

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    void makeDead()
    {
        Destroy(barrier);
        Destroy(gameObject);
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
    }

}
