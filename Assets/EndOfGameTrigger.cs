using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameTrigger : MonoBehaviour
{
    public Player playerGame;
    void OnTriggerEnter2D () {
        playerGame.CompleteLevel();
    }
}
