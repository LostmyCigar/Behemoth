using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CardPosition
{
    inDeck,
    inStore,
    inHandPlayer1,
    inHandPlayer2,
}

public enum CardAction
{
    Neutral,
    isAttacking,
    isDefending
}




public class MonsterCard : MonoBehaviour
{
    [Header("Card Info")]
    public int cardId;
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private string cardEffect;
    [SerializeField] private int startHp;
    [SerializeField] private int startAttack;
    [SerializeField] private int startMutationSpace;
    public StatusEffects _statusEffectApplies;
    public StatusEffects _statusEffectAffected;
    public List<GameObject> MutationCards = new List<GameObject>();

    public int currentHp;
    public int currentAttack;
    public int originalAttack;
    public int currentMutationSpace;

    [Header("Card general info")]
    public bool showCardBack;
    public static bool staticShowCardBack;
    public bool isSelected;
    public bool canAttack;


    [Header("Card store info")]
    public bool isBuyable;
    public int cost;


    [Header("Where is the card and what is it doing?")]
    public CardPosition _cardPosition;
    public CardAction _cardAction;


    [Header("Card Display")]
    public Text nameText;
    public Text descriptionText;
    public Text hpText;
    public Text attackText;
    public Text effectText;
    public GameObject cardBack;

    public GameObject gameManager;
    public PlayerManager playerManager;
    public DatabaseMonsterCards databaseMonsterCards;
    public Player1 player1;
    public Player2 player2;
    DraggableAttack draggable;

    void Start()
    {
        _cardAction = CardAction.Neutral;
        currentHp = startHp;
        currentAttack = startAttack;
        originalAttack = currentAttack;
        currentMutationSpace = startMutationSpace;
        _statusEffectAffected = StatusEffects.None;
        _statusEffectApplies = StatusEffects.None;

        nameText.text = cardName;
        descriptionText.text = cardDescription;
        effectText.text = cardEffect;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        databaseMonsterCards = gameManager.GetComponent<DatabaseMonsterCards>();
        player1 = gameManager.GetComponent<Player1>();
        player2 = gameManager.GetComponent<Player2>();
        draggable = gameObject.GetComponent<DraggableAttack>();
    }


    void Update()
    {
        CardDisplay();
        UppdateCardFacing();
        CalculateCost();
        IsCardBuyable(_cardPosition, playerManager.GetCurrentPlayerAction());
        CanCardAttack();
    }


    private void CanCardAttack()
    {
        if (canAttack)
        {
            draggable.enabled = true;
        }
        else
        {
            draggable.enabled = false;
        }
    }


    public void TryAttacking(GameObject enemy)
    {
        MonsterCard enemyCard = enemy.GetComponent<MonsterCard>();
        if (playerManager.MonsterCardInOtherHand(enemy))
        {
            Attack(enemy);
        }
    }
    public void Attack(GameObject enemy)
    {
        MonsterCard enemyCard = enemy.GetComponent<MonsterCard>();

        enemyCard.TakeDamage(currentAttack, _statusEffectApplies);
        canAttack = false;
        playerManager.CardDoneAttacking();
        
    }

    public void TakeDamage(int damage, StatusEffects statusEffects)
    {
        _statusEffectAffected = statusEffects;
        Debug.Log("Taking damage " + damage);
        currentHp -= DamageAfterModifiers(damage);
    }

    public int DamageAfterModifiers(int damage)
    {
        return damage;
    }

    private void CardDisplay()
    {
        if (attackText.text != currentAttack.ToString())
        {
            attackText.text = currentAttack.ToString();
        }

        if (hpText.text != currentHp.ToString())
        {
            hpText.text = currentHp.ToString();
        }
    }

    private void UppdateCardFacing()
    {
        staticShowCardBack = showCardBack;
        if (staticShowCardBack == true)
        {
            cardBack.SetActive(true);
        }
        else
        {
            cardBack.SetActive(false);
        }
    }

    public void FaceDown()
    {
        showCardBack = true;
    }

    public void FaceUp()
    {
        showCardBack = false;
    }

    public void CalculateCost()
    {
        int cardsOnHand = playerManager.GetCurrentPlayerCardsOnHand();
        if (cardsOnHand < 1)
        {
            cost = 0;
        }
        else if (cardsOnHand == 1)
        {
            cost = 3;
        }
        else if (cardsOnHand == 2)
        {
            cost = 5;
        }
        else if (cardsOnHand == 3)
        {
            cost = 6;
        }
    }


    public void IsCardBuyable(CardPosition cardPosition, PlayerAction playerAction)
    {
        int cardsOnHand = playerManager.GetCurrentPlayerCardsOnHand();
        int playerCurrency = playerManager.GetCurrentPlayerRecruitmentPoints();
        if (cardPosition == CardPosition.inStore && cost <= playerCurrency && cardsOnHand < 4)  //lägg till cost
        {
            if (playerAction == PlayerAction.BuyPhase || playerAction == PlayerAction.ChooseFirstCard)
            {
                isBuyable = true;

            }
            else isBuyable = false;
        }
        else isBuyable = false;
    }

    public void ClickedOn()
    {
        if (_cardPosition == CardPosition.inStore)
        {
            if (playerManager._playerTurnState == PlayerTurnState.Player1 || playerManager._playerTurnState == PlayerTurnState.StartPlayer1)
            {
                player1.TryBuyMonsterCard(this.gameObject);
            }
            if (playerManager._playerTurnState == PlayerTurnState.Player2 || playerManager._playerTurnState == PlayerTurnState.StartPlayer2)
            {
                player2.TryBuyMonsterCard(this.gameObject);
            }
        } 
        else if (_cardPosition == CardPosition.inHandPlayer1)
        {
            UtilityClass utility = gameManager.GetComponent<UtilityClass>();
            utility.SelectMonsterCard(this.gameObject);

        }
        else if (_cardPosition == CardPosition.inHandPlayer2)
        {
            UtilityClass utility = gameManager.GetComponent<UtilityClass>();
            utility.SelectMonsterCard(this.gameObject);
        }

    }
}
