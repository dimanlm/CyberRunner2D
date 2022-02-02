using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimations : MonoBehaviour
{
    public PlayerCharacterController playerCharacterController = null;

    public float HorizontalInput { 
        get{
            if (this.playerCharacterController != null){
                return this.playerCharacterController.horizontalInput;
            }
            return 0f;
        }
    }

    public Animator animator = null;

    // usually used for animations
    void LateUpdate(){ 
        if (this.animator != null){
            this.animator.SetFloat("Horizontal", Mathf.Abs(this.HorizontalInput));
        }
    }
}
