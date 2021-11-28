using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public enum MutationCardPosition
{
    inDeck,
    inStore,
    onMonsterCard,
}
public class MutationCard : MonoBehaviour
{
    [Header("Card Info")]
    public int cardId;
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    public int cardCost;

    [Header("Card Display")]
    public Text nameText;
    public Text descriptionText;
    public Text costText;

    [Header("Where is the card?")]
    public MutationCardPosition _cardPosition;


    public GameObject gameManager;
    public PlayerManager playerManager;
    public DatabaseMutationCards databaseMutationCards;
    public MonsterCard monsterCard;
    public Player1 player1;
    public Player2 player2;
    public DraggableBuy draggable;
    void Start()
    {
        nameText.text = cardName;
        descriptionText.text = cardDescription;
        costText.text = cardCost.ToString();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        databaseMutationCards = gameManager.GetComponent<DatabaseMutationCards>();
        player1 = gameManager.GetComponent<Player1>();
        player2 = gameManager.GetComponent<Player2>();
    }


    void Update()
    {
        CheckPosition();
    }


    public void CheckPosition()
    {
        if (_cardPosition == MutationCardPosition.inStore && playerManager.GetCurrentPlayerAction() == PlayerAction.BuyPhase)
        {
            draggable.enabled = true;
        }
        else draggable.enabled = false;
    }

    public void TryPlacingOn(GameObject card)
    {
        MonsterCard monster = card.GetComponent<MonsterCard>();
        if (playerManager.MonsterCardInSameHand(card) && monster.currentMutationSpace > 0)
        {
            if (playerManager._playerTurnState == PlayerTurnState.Player1 && player1.mutationPoints > cardCost)
            {
                PlaceOn(card);
            }
            if (playerManager._playerTurnState == PlayerTurnState.Player2 && player2.mutationPoints > cardCost)
            {
                PlaceOn(card);
            }
        }
    }

    private void PlaceOn(GameObject card)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();
        Debug.Log("Tried placing on " + card.name + " in " + monsterCard._cardPosition);

    }
}