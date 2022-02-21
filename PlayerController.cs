using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject focalPoint;

    private Animator animator;

    public float gravityModifier;
    public float horizontalInput;
    public float forwardInput;

    private bool onGround = true;
    private bool isAttacking;

    public float mouseAxis;
    public float speed;
    public float forceSpeed;
    public float boostStrength = 400.0f;
    public float jumpForce = 10.0f;
    private float xBound = 24;
    private float zBound = 24;
    private float rotationSpeed = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerPosition();
        MovePlayer();
    }

    //player movement
    void MovePlayer()
    {
        // rotation


        mouseAxis = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseAxis * rotationSpeed * Time.deltaTime);

        // move foward and side (rotation is on FollowPlayer script)

        forwardInput = Input.GetAxis("Vertical");

        var velocityF = focalPoint.transform.forward * forwardInput * speed;
        playerRb.AddForce(velocityF.normalized * forceSpeed * Time.deltaTime, ForceMode.VelocityChange);

        animator.SetFloat("Speed", velocityF.magnitude * speed);


        horizontalInput = Input.GetAxis("Horizontal");
        var velocityH = focalPoint.transform.right * horizontalInput * speed;

        if (horizontalInput < 0)
        {
            horizontalInput = -1;
            transform.Translate(velocityH * Time.deltaTime);
            animator.SetFloat("Speed", -velocityH.magnitude);

        } else if (horizontalInput > 0)
        {
            horizontalInput = 1;
            transform.Translate(velocityH * Time.deltaTime);
            animator.SetFloat("Speed", -velocityH.magnitude);

        }

        // attack

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", true);
            isAttacking = true;
        }
        else
        {
            animator.SetBool("Attack", false);
            isAttacking = false;
        }

        


        // jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;

            if (!onGround)
            {
                animator.SetBool("Jump", true);
            }
        }

        animator.SetFloat("JumpVel", playerRb.velocity.y);

        //BoostSpeed();
    }

    // player boost speed
    void BoostSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            playerRb.AddForce(focalPoint.transform.forward * boostStrength * Time.deltaTime, ForceMode.Impulse);
        }
    }

    // Limit player movement
    void ConstrainPlayerPosition()
    {
        // bounds x
        if (transform.position.x >= xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        // bounds z
        if (transform.position.z >= xBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z <= -xBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }

    // Jump only when on ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("Jump", false);
        }

        /*if (collision.gameObject.tag == "Enemy" && isAttacking)
        {
            Debug.Log("attacking");
        }*/
    }

}
