using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform targetTransform = null;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {   
        // nullcheck
        if (this.targetTransform != null){
            // affect camera's position
            this.transform.position = new Vector3(this.targetTransform.position.x, this.targetTransform.position.y, -10);
        }
    }
}
