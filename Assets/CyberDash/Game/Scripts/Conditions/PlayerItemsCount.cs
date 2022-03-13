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

    void Update()
    {
        if (this.PlayerInventory != null && this.PlayerInventory.cardCount > 0)
        {
            // Show
            this.Show();

            // Update
            if (this.cardText != null)
                this.cardText.text = this.PlayerInventory.cardCount.ToString();
        }
        else
        {
            // Hide view
            this.Hide();
        }
    }

    void Show()
    {
        if (this.cardImage != null)
            this.cardImage.gameObject.SetActive(true);
        if (this.cardText != null)
            this.cardText.gameObject.SetActive(true);
    }

    void Hide()
    {
        if (this.cardImage != null)
            this.cardImage.gameObject.SetActive(false);
        if (this.cardText != null)
            this.cardText.gameObject.SetActive(false);
    }
}
