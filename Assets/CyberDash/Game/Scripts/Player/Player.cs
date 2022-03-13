using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   

    public Vector3 spawnPosition = new Vector3();

    // Components
    public PlayerCharacterController playerCharacterController = null;
    public FollowPosition cameraFollowPosition = null;

    // UI Death screen
    public GameObject deathScreen = null;
    public GameObject endOfGameScreen = null;
    public GameObject inventoryUI = null;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnPosition = this.playerCharacterController.transform.position;
        this.deathScreen.SetActive(false);
        this.endOfGameScreen.SetActive(false);
        this.inventoryUI.SetActive(true);
    }

    public void Die(){
        this.deathScreen.SetActive(true);
        this.inventoryUI.SetActive(false);

        // PLayer controller: Stop recieving input
        this.playerCharacterController.enabled = false;

        // Player controller: Camera stops following the player
        this.cameraFollowPosition.enabled = false;
    }

    public void Respawn(){
        // hide death screen
        this.deathScreen.SetActive(false);
        // hide end of the game screen
        this.endOfGameScreen.SetActive(false);
        this.inventoryUI.SetActive(true);

        // Character: teleported back to the spawn point
        this.playerCharacterController.transform.position = this.spawnPosition;

        // Rigid body
        this.playerCharacterController.Rigidbody2D.velocity = Vector3.zero;

        // Player controller: Enable previous features after death
        this.playerCharacterController.enabled = true;
        this.cameraFollowPosition.enabled = true;
    }

    public void CompleteLevel () 
    {
        this.endOfGameScreen.SetActive(true);
        this.inventoryUI.SetActive(false);
    }
}
