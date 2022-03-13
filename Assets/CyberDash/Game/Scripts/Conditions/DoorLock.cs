using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    // Door
    public Platform doorMovingPlatform = null;

    // Flag
    private bool opened = false;

    void Start()
    {
        // Disable door to prevent it from moving
        if (this.doorMovingPlatform != null)
            this.doorMovingPlatform.enabled = false;
    }

    // Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent potential issues
        if (this.opened == true)
            return;

        // Try to find a player with an inventory attached
        PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>();
        if (playerInventory != null)
        {
            // If enough keys
            if (playerInventory.cardCount > 0)
            {
                // Remove a key from inventory
                playerInventory.cardCount -= 1;

                // Enabling door will make it move
                if (this.doorMovingPlatform != null)
                    this.doorMovingPlatform.enabled = true;

                // Unset flag
                this.opened = false;

                // Delete key from scene (and prevent further use)
                // GameObject.Destroy(this.gameObject);
            }
        }
    }
}
