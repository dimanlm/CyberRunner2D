using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
    // Flag
    private bool canBeGrabbed = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent potential issues
        if (this.canBeGrabbed == false)
            return;

        // Try to find a player with an inventory attached
        PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>();
        if (playerInventory != null)
        {
            // Attribute key to inventory
            playerInventory.cashCount += 1;
            
            // play the collect sound
            FindObjectOfType<AudioManager>().Play("collect");
            
            // Unset flag
            this.canBeGrabbed = false;

            // Delete key from scene (and prevent further use)
            GameObject.Destroy(this.gameObject);
        }
    }
}
