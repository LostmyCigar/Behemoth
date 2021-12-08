using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum MutationCardPosition
{
    inDeck,
    inStore,
    onMonsterCard,
    inGraveyard
}
public class MutationCard : MonoBehaviour
{
    [Header("Card Info")]
    public int cardId;
    public string cardName;
    public string cardDescription;
    public int cardCost;

    [Header("Card Display")]
    public Text nameText;
    public Text descriptionText;
    public Text costText;

    [Header("Where is the card?")]
    public MutationCardPosition _cardPosition;

    [Header("Retrieve on Start")]
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
        if (playerManager.MonsterCardInCurrentPlayerHand(card) && monster.currentMutationSpace > 0)
        {
            if (playerManager.GetCurrentPlayerMutationPoints() >= cardCost)
            {
                PlaceOn(card);
            }
        }
    }

    public void UppdateForGraveyard()
    {
        cardCost = 2;
        _cardPosition = MutationCardPosition.inGraveyard;
    }

    private void PlaceOn(GameObject card)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();


        int i = databaseMutationCards.MutationCardStore.IndexOf(gameObject);
        monsterCard.MutationCards.Add(gameObject);

        databaseMutationCards.MutationCardStore.Remove(databaseMutationCards.MutationCardStore[i]);
        databaseMutationCards.NewCardToStore();

        playerManager.SetCurrentPlayerMutationPoints(playerManager.GetCurrentPlayerMutationPoints() - cardCost);
        monsterCard.currentMutationSpace--;
        monsterCard.UppdateMutationCards();
    }

    public void Hide()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
