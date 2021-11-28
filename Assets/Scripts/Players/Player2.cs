using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Player2 : MonoBehaviour
{
    [Header("Info")]
    public int mutationPoints;
    public int recruitmentPoints;
    public int currentCardsOnHand;
    public int maximumCardsOnHand;
    public int attacksAvailable;

    public int deadCards;
    public int cardsDoneAttacking;

    [Header("State")]
    public PlayerAction _playerAction;

    [Header("Add in inspector")]
    public PlayerManager playerManager;
    public DatabaseMonsterCards databaseMonsterCards;


    [Header("UI")]
    public Image player2Hand;
    public Text recruitmentPointText;
    public Text mutationPointText;

    [Header("Cards")]
    public List<GameObject> Player2MonsterCards = new List<GameObject>();



    private void Awake()
    {
        _playerAction = PlayerAction.ChooseFirstCard;
    }
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        databaseMonsterCards = GetComponent<DatabaseMonsterCards>();
    }

    void Update()
    {
        UIDisplay();
        PlayerActions();
        HandleCards();
    }

    private void UIDisplay()
    {
        recruitmentPointText.text = recruitmentPoints.ToString();

        mutationPointText.text = mutationPoints.ToString();
    }

    private void PlayerActions()
    {
        if (_playerAction == PlayerAction.ChooseFirstCard)
        {
            if (currentCardsOnHand == 1)
            {
                playerManager.EndTurn(true);
            }
        }
        if (_playerAction == PlayerAction.AttackPhase)
        {
            if (cardsDoneAttacking == Player2MonsterCards.Count)
            {
                StopAttack();
            }
        }
    }

    public void Attack()
    {
        for (int i = 0; i < Player2MonsterCards.Count; i++)
        {
            MonsterCard monsterCard = Player2MonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.originalAttack = monsterCard.currentAttack;

        }

        while (attacksAvailable > Player2MonsterCards.Count)
        {
            int j = 0;
            for (int i = attacksAvailable; i > Player2MonsterCards.Count; i--)
            {
                MonsterCard monsterCard = Player2MonsterCards[j].GetComponent<MonsterCard>();
                monsterCard.currentAttack++;
                j++;
                attacksAvailable--;
                if (j >= Player2MonsterCards.Count)
                {
                    j = 0;
                }
            }
        }

        if (attacksAvailable > 0)
        {
            int y = 0;
            for (int i = attacksAvailable; i > 0; i--)
            {

                MonsterCard monsterCard = Player2MonsterCards[y].GetComponent<MonsterCard>();
                monsterCard.canAttack = true;
                y++;
                attacksAvailable--;
            }
        }
    }

    private void StopAttack()
    {
        for (int i = 0; i < Player2MonsterCards.Count; i++)
        {
            MonsterCard monsterCard = Player2MonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.currentAttack = monsterCard.originalAttack;
        }
        cardsDoneAttacking = 0;
        playerManager.MovePlayerToNextPhase();

    }

    public void TryBuyMonsterCard(GameObject card)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();
        if (monsterCard.isBuyable && Player2MonsterCards.Count < maximumCardsOnHand)
        {
            int i = databaseMonsterCards.MonsterCardStore.IndexOf(card);
            Player2MonsterCards.Add(databaseMonsterCards.MonsterCardStore[i]);

            databaseMonsterCards.MonsterCardStore.Remove(databaseMonsterCards.MonsterCardStore[i]);
            databaseMonsterCards.NewCardToStore();

            currentCardsOnHand++;
            recruitmentPoints = recruitmentPoints - monsterCard.cost;
        }
    }
    private void HandleCards()
    {
        for (int i = 0; i < Player2MonsterCards.Count; i++)
        {
            Player2MonsterCards[i].transform.SetParent(player2Hand.transform);
            MonsterCard monsterCard = Player2MonsterCards[i].GetComponent<MonsterCard>();
            monsterCard._cardPosition = CardPosition.inHandPlayer2;

            if (_playerAction == PlayerAction.AttackPhase)
            {
                monsterCard._cardAction = CardAction.isAttacking;
            }
        }
    }

}
