using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PlayerAction
{
    ChooseFirstCard,
    ChooseAction,
    AttackPhase,
    BuyPhase,
}
public class Player1 : MonoBehaviour
{
    [Header("Info")]
    public int mutationPoints;
    public int recruitmentPoints;
    public int currentCardsOnHand;
    public int maximumCardsOnHand;
    public int attacksAvailable;

    public int deadCards;
    public int cardsAttacking;
    public int cardsDoneAttacking;

    [Header("State")]
    public PlayerAction _playerAction;

    [Header("Added on start")]
    public PlayerManager playerManager;
    public DatabaseMonsterCards databaseMonsterCards;
    public DatabaseWeaponCards databaseWeaponCards;

    [Header("UI")]
    public Image player1Hand;
    public Image player1Weapons;
    public Text recruitmentPointText;
    public Text mutationPointText;

    [Header("Cards")]
    public List<GameObject> Player1MonsterCards = new List<GameObject>();
    public List<GameObject> Player1WeaponCards = new List<GameObject>();



    private void Awake()
    {
        _playerAction = PlayerAction.ChooseFirstCard;
    }
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        databaseWeaponCards = GetComponent<DatabaseWeaponCards>();
        databaseMonsterCards = GetComponent<DatabaseMonsterCards>();
    }

    void Update()
    {
        UIDisplay();
        PlayerActions();
        HandleMonsterCards();
        HandleWeaponCards();
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
            if (cardsDoneAttacking == Player1MonsterCards.Count)
            {
                StopAttack();
                cardsAttacking = 0;
            }
            if (cardsDoneAttacking != 0 && cardsDoneAttacking == cardsAttacking)
            {
                StopAttack();
                cardsAttacking = 0;
            }
        }
    }

    public void Attack()
    {
        for (int i = 0; i < Player1MonsterCards.Count; i++)
        {
            MonsterCard monsterCard = Player1MonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.originalAttack = monsterCard.currentAttack;
        }

        while (attacksAvailable > Player1MonsterCards.Count)
        {
            int j = 0;
            for (int i = attacksAvailable; i > Player1MonsterCards.Count; i--)
            {
                MonsterCard monsterCard = Player1MonsterCards[j].GetComponent<MonsterCard>();
                monsterCard.currentAttack++;
                j++;
                attacksAvailable--;
                if (j >= Player1MonsterCards.Count)
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

                MonsterCard monsterCard = Player1MonsterCards[y].GetComponent<MonsterCard>();
                monsterCard.canAttack = true;
                y++;
                attacksAvailable--;
                cardsAttacking++;
            }
        }
    }

    private void StopAttack()
    {
        for (int i = 0; i < Player1MonsterCards.Count; i++)
        {
            MonsterCard monsterCard = Player1MonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.currentAttack = monsterCard.originalAttack;
        }
        cardsDoneAttacking = 0;
        playerManager.MovePlayerToNextPhase();

    }

    public void TryBuyMonsterCard(GameObject card)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();
        if (monsterCard.isBuyable && Player1MonsterCards.Count < maximumCardsOnHand)
        {
            int i = databaseMonsterCards.MonsterCardStore.IndexOf(card);
            Player1MonsterCards.Add(databaseMonsterCards.MonsterCardStore[i]);

            databaseMonsterCards.MonsterCardStore.Remove(databaseMonsterCards.MonsterCardStore[i]);
            databaseMonsterCards.NewCardToStore();

            currentCardsOnHand++;
            recruitmentPoints = recruitmentPoints - monsterCard.cost;
        }
    }

    public void TryBuyWeaponCard(GameObject card)
    {
        WeaponCard weaponCard = card.GetComponent<WeaponCard>();
        if (weaponCard.cardCost <= recruitmentPoints && currentCardsOnHand >= 4)
        {
            int i = databaseWeaponCards.WeaponCardStore.IndexOf(card);
            Player1WeaponCards.Add(databaseWeaponCards.WeaponCardStore[i]);

            databaseWeaponCards.WeaponCardStore.Remove(databaseWeaponCards.WeaponCardStore[i]);
            databaseWeaponCards.NewCardToStore();

            recruitmentPoints = recruitmentPoints - weaponCard.cardCost;
        }
    }

    private void HandleMonsterCards()
    {
        for (int i = 0; i < Player1MonsterCards.Count; i++)
        {
            Player1MonsterCards[i].transform.SetParent(player1Hand.transform);
            MonsterCard monsterCard = Player1MonsterCards[i].GetComponent<MonsterCard>();
            monsterCard._cardPosition = CardPosition.inHandPlayer1;

            if (monsterCard.canAttack)
            {
                monsterCard._cardAction = CardAction.isAttacking;
            }
        }
    }

    private void HandleWeaponCards()
    {
        for (int i = 0; i < Player1WeaponCards.Count; i++)
        {
            Player1WeaponCards[i].transform.SetParent(player1Weapons.transform);
            WeaponCard weaponCard = Player1WeaponCards[i].GetComponent<WeaponCard>();
            weaponCard._cardPosition = CardPosition.inHandPlayer1;
        }
    }
}
