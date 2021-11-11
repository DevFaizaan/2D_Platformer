using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    //movement variable
    public float maxSpeed;

    //jumping variables
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;
    float mx;

    //dashing
    public float dashDistance;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;


    //for shooting
    public Transform gunTip;
    public GameObject bullet;
    public GameObject upgradedBullet;
    public float fireRate = 0.5f;
    float nextFire = 0f;
    bool isUpgraded = false;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        facingRight = true;

        
    }

    // Update is called once per frame

    void Update()
    {
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
        }
            if (Input.GetKeyDown(KeyCode.A)){
                if(doubleTapTime > Time.time && lastKeyCode == KeyCode.A) {
                    StartCoroutine(Dash(-1f));
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.A;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
                {
                    StartCoroutine(Dash(1f));
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.D;
            }

        

        //player shooting
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            myAnim.SetTrigger("attack");
            fireRocket();
        }
    }



    void FixedUpdate()
    {
      
        //check if we are grounded, if not then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);
        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);




        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));
        if (!isDashing)
        {
            myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);
        }
       

        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }


       
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void fireRocket()
    {
        if(isUpgraded == false)
        {
            if (Time.time > nextFire)
            {
                
                nextFire = Time.time + fireRate;
                if (facingRight)
                {
                    Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (!facingRight)
                {
                    Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));

                }
            }
        }
        else if(isUpgraded == true)
        {
            if (Time.time > nextFire)
            {
                
                nextFire = Time.time + fireRate;
                if (facingRight)
                {
                    Instantiate(upgradedBullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else if (!facingRight)
                {
                    Instantiate(upgradedBullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));

                }
            }
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovementBoost"))
        {
            maxSpeed = 7f;
            jumpHeight = 175f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Destroy(other.gameObject);
            StartCoroutine(resetPowerUp1());

        }
        if (other.CompareTag("attackSpeed"))
        {
            fireRate = 0.5f;
            Destroy(other.gameObject);
            StartCoroutine(resetPowerUp2());

        }
        if (other.CompareTag("attackDamage"))
        {
            isUpgraded = true;
            Destroy(other.gameObject);
            StartCoroutine(resetPowerUp3());
        }


    }

    public IEnumerator resetPowerUp1()
    {
        yield return new WaitForSeconds(5);
        
            maxSpeed = 4f;
            jumpHeight = 100f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public IEnumerator resetPowerUp2()
    {
        yield return new WaitForSeconds(5);
        fireRate = 0.8f;



    }
    public IEnumerator resetPowerUp3()
    {
        yield return new WaitForSeconds(5);
        isUpgraded = false;



    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        myAnim.SetBool("isDash", isDashing);
        myRB.velocity = new Vector2(myRB.velocity.x, 0f);
        myRB.AddForce(new Vector2(dashDistance * direction, 0), ForceMode2D.Impulse);
        float gravity = myRB.gravityScale;
        myRB.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        myAnim.SetBool("isDash", isDashing);
        myRB.gravityScale = gravity;
    }






}



