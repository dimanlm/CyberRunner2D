using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // initialization
    public Transform startY = null;
    public Transform endY = null;
    public float moveSpeed = 1f;
    //
    public float percent = 0.0f; // 0.0 <-> 1.0
    //
    public bool moveDirection = false;
    public bool loop = true;
    // Rigid body
    public Rigidbody2D Rigidbody2D = null;


    // Update is called once per frame
    void Update()
    {
        // Goes towards end
            if (this.moveDirection == true)
            {
                // Move
                this.percent = Mathf.Clamp01(this.percent + moveSpeed * Time.deltaTime);

                // Reached end position
                if (this.loop == true)
                {
                    if (this.percent == 1.0f)
                    {
                        // Invert direction
                        this.moveDirection = !this.moveDirection;
                    }
                }
            }
            // Goes towards start
            else
            {
                // Move
                this.percent = Mathf.Clamp01(this.percent - moveSpeed * Time.deltaTime);

                // Reached start position
                if (this.loop == true)
                {
                    if (this.percent == 0.0f)
                    {
                        // Invert direction
                        this.moveDirection = !this.moveDirection;
                    }
                }
            }
    }

    void FixedUpdate()
    {
        if (this.Rigidbody2D != null){
            // Lerp: linear interpolation
            Vector2 newPosition = Vector2.Lerp(this.startY.position, this.endY.position, this.percent);
            this.Rigidbody2D.MovePosition(newPosition);
        }
    }

    void OnDrawGizmos(){ 
        Gizmos.DrawLine(this.startY.position, this.endY.position);
    }
}

