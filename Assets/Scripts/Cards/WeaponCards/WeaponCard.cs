using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WeaponCard : MonoBehaviour
{
    [Header("Card Info")]
    public int cardId;
    public string cardName;
    public string cardDescription;
    public int cardCost;
    public bool canUse;

    [Header("Card Display")]
    public Text nameText;
    public Text descriptionText;
    public Text costText;

    [Header("Where is the card and what is it doing?")]
    public CardPosition _cardPosition;

    [Header("Retrieve on Start")]
    public GameObject gameManager;
    public PlayerManager playerManager;
    public DatabaseWeaponCards databaseWeaponCards;
    public WeaponCard weaponCard;
    public Player1 player1;
    public Player2 player2;
    public DraggableEffect draggable;
    void Start()
    {
        nameText.text = cardName;
        descriptionText.text = cardDescription;
        costText.text = cardCost.ToString();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        databaseWeaponCards = gameManager.GetComponent<DatabaseWeaponCards>();
        player1 = gameManager.GetComponent<Player1>();
        player2 = gameManager.GetComponent<Player2>();
        draggable = gameObject.GetComponent<DraggableEffect>();
    }
    private void Update()
    {
        CanUseEffect();
    }
    private void CanUseEffect()
    {
        if (canUse && playerManager.WeaponCardInCurrentPlayerHand(gameObject))
        {
            draggable.enabled = true;
        }
        else
        {
            draggable.enabled = false;
        }
    }
    public virtual void TryTargeting(GameObject target)
    {
        Debug.Log("WeaponCard " + name + " tried targeting " + target.name);
        Effect(target);
    }
    public virtual void Effect(GameObject target)
    {
        Debug.Log("Effect used");
    }

    public void ClickedOn()
    {
        if (_cardPosition == CardPosition.inStore)
        {
            if (playerManager._playerTurnState == PlayerTurnState.Player1 || playerManager._playerTurnState == PlayerTurnState.StartPlayer1)
            {
                player1.TryBuyWeaponCard(this.gameObject);
            }
            if (playerManager._playerTurnState == PlayerTurnState.Player2 || playerManager._playerTurnState == PlayerTurnState.StartPlayer2)
            {
                player2.TryBuyWeaponCard(this.gameObject);
            }
        }
    }

    public void UppdateCardEndOfTurn()
    {
        canUse = true;
    }
}
