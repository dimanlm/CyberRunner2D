using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMvt : MonoBehaviour
{
    [Header("Ladder Movement")]
    public float verticalInput;
    public float ladderMvtSpeed = 8f;
    public bool isLadder;
    public bool isClimbing;

    private Animator anim;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.anim.speed = 1;
        this.verticalInput = Input.GetAxisRaw("Vertical");
        
        if (this.isLadder && Mathf.Abs(this.verticalInput)>0f){
            this.isClimbing = true;
            this.anim.SetBool("climbing", true);
        }else if (this.isClimbing && Mathf.Abs(this.verticalInput)==0f){
            
            this.anim.speed = 0;
        
        }
    }

    private void FixedUpdate(){
        if (isClimbing){
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * ladderMvtSpeed);
        }else {
            rb.gravityScale = 3f;
        }
    }

    // usually used for animations
    void LateUpdate(){ 
        if (this.anim != null){
            this.anim.SetFloat("Vertical", Mathf.Abs(this.verticalInput));
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Ladder")){
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if (collider.CompareTag("Ladder")){
            isLadder = false;
            isClimbing = false;
            anim.SetBool("climbing", false);
        }
    }
}
