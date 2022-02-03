using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    /**********************************************************************/

    // Rigidbody contains functions to apply pysical movement
    public Rigidbody2D Rigidbody2D = null;

    // velocity
    private Vector3 velocity = Vector3.zero;

    // behavior
    public float moveSpeedFactor = 10.0f;

    // Move damp
    public float moveDampFactor = 0.0f;

    // input
    [Range(-1f,1f)]
    public float horizontalInput = 0f;
    public bool jumpInput = false;

    private Animator anim;
    
    //jump force
    public float jumpForce = 800f;

    /**********************************************************************/

    private void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("grounded", true);
    }

    void Update(){
        // input for horizontal movement
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
        //input for jumping
        this.jumpInput = Input.GetKeyDown(KeyCode.Space);

        // handle flip
        this.HandleFlip();

        // Handle jump
        if (this.jumpInput == true && this.isGrounded==true){
            this.Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }

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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.groundChecker.transform.position, 0.4f, this.groundCheckLayersMask);
            if (colliders != null && colliders.Length>0){ 
                for (int i = 0; i < colliders.Length; i++){
                    // check if the game object is not the player
                    if (colliders[i].gameObject != this.gameObject){
                        this.isGrounded = true;
                        anim.SetBool("grounded", true);
                        // console.Log("ground " + this.isGrounded);
                    }
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.groundChecker.transform.position, 0.4f);
    }
}
