using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    /**********************************************************************/

    // Rigidbody contains functions to apply pysical movement
    public Rigidbody2D Rigidbody2D = null;

    // velocity
    [Header("Movement")]
    private Vector3 velocity = Vector3.zero;

    // behavior
    public float moveSpeedFactor = 10.0f;

    // Move damp
    public float moveDampFactor = 0.0f;

    // input
    [Header("Input")]
    [Range(-1f,1f)]
    public float horizontalInput = 0f;
    [Range(-1f,1f)]
    public float verticalInput = 0f;

    private Animator anim;
    
    //jump force
    [Header("Jumps")]
    public float hangTime = 0.2f;
    private float hangCounter;
    public float jumpForce = 800f;
    public float doubleJumpForce = 400f;
    public bool canDoubleJump = false;


    private bool onPlatform;
    private bool onPlatformLastFrame = false;


    /**********************************************************************/

    private void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("grounded", true);
        anim.SetBool("doublejump", false);
        anim.SetBool("climbing", false);
    }

    void Update(){
        // input for horizontal movement
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
        // input for vertical mvt
        // this.verticalInput = Input.GetAxisRaw("Vertical");    

        // handle flip
        this.HandleFlip();

        anim.SetBool("doublejump", false);

        // Handle jump
        if (isGrounded)
        {
            this.hangCounter = this.hangTime;
        }
        else
        {
            this.hangCounter -= Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))){
            this.Jump();
        }

        if (this.isGrounded == true && onPlatformLastFrame == false)
        {
            FindObjectOfType<AudioManager>().Play("jumpLand");
        }
        onPlatformLastFrame = this.isGrounded;


        // if grounded is false, the you will see the flying animation
        if (this.isGrounded==false) {
            anim.SetBool("grounded", false);
        }
    }

    // function for physics
    void FixedUpdate(){
        // check if grounded
        this.UpdateGroundedStatus();

        // Handle Horizontal moves
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector3(this.horizontalInput * this.moveSpeedFactor, this.Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            this.Rigidbody2D.velocity = Vector3.SmoothDamp(this.Rigidbody2D.velocity, targetVelocity, ref velocity, this.moveDampFactor);
        }
    }

    private void Jump()
    {
        if (this.hangCounter>0)
        {
            this.Rigidbody2D.velocity = new Vector2(this.Rigidbody2D.velocity.x, jumpForce);
            FindObjectOfType<AudioManager>().Play("jumpStart");
            canDoubleJump = true;
        }
        else if (this.canDoubleJump)
        {
            this.Rigidbody2D.velocity = new Vector2(this.Rigidbody2D.velocity.x, doubleJumpForce);
            anim.SetBool("doublejump", true);
            FindObjectOfType<AudioManager>().Play("doubleJump");
            this.canDoubleJump = false;
        }
    }

    // Flip
    private bool facingRight = true;

    private void HandleFlip(){
        if (this.horizontalInput>0 && facingRight==false) {
            Flip();
        }else if (this.horizontalInput<0 && facingRight==true) {
            Flip();
        }
    }
    
    private void Flip(){
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;
        // multiply the player's local scale by -1
        Vector3 invertedScale = transform.localScale;
        invertedScale.x *= -1;
        // Apply
        transform.localScale = invertedScale;
    }

    // Ground check
    [Header("Physics")]
    public Transform groundChecker = null;
    public bool isGrounded = false;
    public LayerMask groundCheckLayersMask;

    private void UpdateGroundedStatus(){ 
        // unset flag 
        this.isGrounded = false;
        if (this.groundChecker != null){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.groundChecker.transform.position, 0.2f, this.groundCheckLayersMask);
            if (colliders != null && colliders.Length>0){ 
                for (int i = 0; i < colliders.Length; i++){
                    // check if the game object is not the player
                    if (colliders[i].gameObject != this.gameObject){
                        this.isGrounded = true;
                        anim.SetBool("grounded", true);
                    }
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.groundChecker.transform.position, 0.2f);
    }


    // play animation sounds
    private void runSound1(){
        FindObjectOfType<AudioManager>().Play("run1");
    }

    private void runSound2(){
        FindObjectOfType<AudioManager>().Play("run2");
    }

    private void climbSound1(){
        FindObjectOfType<AudioManager>().Play("climb1");
    }

    private void climbSound2(){
        FindObjectOfType<AudioManager>().Play("climb2");
    }

}
