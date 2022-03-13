using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsCount : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory = null;
    public PlayerInventory PlayerInventory
    {
        get
        {
            if (this.playerInventory == null)
                this.playerInventory = this.gameObject.GetComponentInParent<PlayerInventory>();
            return this.playerInventory;
        }
    }

    [Header("UI References")]
    public Image cardImage = null;
    public Text cardText = null;
    public Image cashImage = null;
    public Text cashText = null;

    void Update()
    {
        if (this.PlayerInventory != null)
        {
            // Show
            this.Show();

            // Update
            if (this.cardText != null)
            {
                this.cardText.text = this.PlayerInventory.cardCount.ToString();
            }
            
            if (this.cashText != null)
            {
                this.cashText.text = this.PlayerInventory.cashCount.ToString();
            }
        }
    }

    void Show()
    {
        if (this.cardImage != null)
            this.cardImage.gameObject.SetActive(true);
        if (this.cardText != null)
            this.cardText.gameObject.SetActive(true);

        if (this.cashImage != null)
            this.cashImage.gameObject.SetActive(true);
        if (this.cashText != null)
            this.cashText.gameObject.SetActive(true);
    }

}
