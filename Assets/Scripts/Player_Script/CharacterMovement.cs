using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    private new Rigidbody rigidbody;
    private Animator anim;

    [SerializeField] float jumpSpeed = 600.0f;
    [SerializeField] float startingJumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float knifeSpeed = 600.0f;
    public Transform knifeSpawn;
    public Rigidbody knifePrefab;
    Rigidbody clone;

    private new AudioSource audio;
    public AudioClip jumpAudio;
    public AudioClip knifeAudio;

  
    [SerializeField] float currentJumpSpeed;

    public float JumpSpeed
    {
        get { return jumpSpeed; }
        set
        {
            if (value < 0)
                jumpSpeed = 0;
            else
                jumpSpeed = value;
        }
    }
    




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        knifeSpawn = GameObject.Find("KnifeSpawn").transform;
        audio = GetComponent<AudioSource>();
        jumpSpeed = startingJumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        print("Update");
        moveDirection = Input.GetAxis("Horizontal");
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("IsJumping"); 
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
            audio.PlayOneShot(jumpAudio);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection));



    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Attack()
    {
        anim.SetTrigger("Attacking");
        audio.PlayOneShot(knifeAudio);
        
    }

    public void CallFireProjectile()
    {
        clone = Instantiate(knifePrefab, knifeSpawn.position, knifeSpawn.rotation) as Rigidbody;
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
    }

    public void PowerUpJump()
    {
        jumpSpeed += 200.0f;        
    }

    public void ResetPowerUp()
    {
        jumpSpeed = startingJumpSpeed;
    }

    public void test()
    {
        print("hooa");
    }
}
