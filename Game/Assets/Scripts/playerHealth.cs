using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerHealth : MonoBehaviour
{

    public float fullHealth;
    public float currentHealth;
    public GameObject deathEffect;
   

    public AudioClip playerHurt;
    Animator playerAnimator;
    private SpriteRenderer player;

    CharacterController controlMovement;

    AudioSource playerAS;

    //HUD Varaibles
    public Slider healthSlider;
    public Image damageScreen;

    bool damaged;
    public float iFramesDuration;
    public int noOfFlashes;
    Color damagedColour = new Color(0f, 0f, 0f, 0.5f);
    float smoothColour = 5f;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;

        controlMovement = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<SpriteRenderer>();

        //HUD Initialisation
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        damaged = false;

        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (damaged)
        {
            damageScreen.color = damagedColour;
        }
        else
       {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColour * Time.deltaTime);
            
        }
        damaged = false;
    }


    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        playerAS.clip = playerHurt;
        playerAS.Play();
        playerAS.PlayOneShot(playerHurt);
        damaged = true;
        StartCoroutine(invincibility());

        if(currentHealth <=0)
        {
            playerAnimator.SetTrigger("isDead");
            makeDead();
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator invincibility()
    {
        
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < noOfFlashes; i++)
        {
            player.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (noOfFlashes * 2));
            player.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (noOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
        
    }

    public void addHealth(float healthAmount) {
        currentHealth += healthAmount;
        if(currentHealth > fullHealth) currentHealth = fullHealth;

        healthSlider.value = currentHealth;
           
    }

    public void makeDead()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
