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
    [SerializeField] private float startHp;
    [SerializeField] private int startAttack;
    [SerializeField] private int startMutationSpace;

    [Header("Status Effects")]
    public StatusEffectStates _statusEffectApplies;
    public int appliesTimer;
    public StatusEffectStates _statusEffectAffected;
    public int affectedTimer;

    [Header("Mutation Cards Active")]
    public List<GameObject> MutationCards = new List<GameObject>();

    [Header("Current Stats")]
    public float currentHp;
    public int currentAttack;
    public int originalAttack;
    public int currentMutationSpace;

    [Header("Reviving")]
    public bool willRevive;
    public float hpAfterRevive;
    public int attackAfterRevive;

    [Header("Healing")]
    public bool cantBeHealed;
    public bool canHealAllies;
    public int healOnAttack;

    [Header("Damage reduction")]
    public int flatDamageReduction;
    public float percentageDamageReduction;

    [Header("Double attacking")]
    public int howManyAttacks;
    private int attacksLeft;

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

    [Header("Card Display Mutationcards")]
    public GameObject MutationCard1;
    public GameObject MutationCard2;
    public GameObject MutationCard3;
    public Text Mutationcard1NameText;
    public Text Mutationcard2NameText;
    public Text Mutationcard3NameText;

    [Header("Retrieve on Start")]
    public GameObject gameManager;
    public PlayerManager playerManager;
    public DatabaseMonsterCards databaseMonsterCards;
    public Player1 player1;
    public Player2 player2;
    DraggableAttack draggable;
    public MutationCardEffect mutationCardEffect;
    void Awake()
    {
        if (gameObject.tag == "Untagged")
        {
            Debug.Log(gameObject.name + " is untagged");
        }

        _cardAction = CardAction.Neutral;
        currentHp = startHp;
        currentAttack = startAttack;
        originalAttack = currentAttack;
        currentMutationSpace = startMutationSpace;
        attacksLeft = howManyAttacks;
        _statusEffectAffected = StatusEffectStates.None;
        _statusEffectApplies = StatusEffectStates.None;

        nameText.text = cardName;
        descriptionText.text = cardDescription;
        effectText.text = cardEffect;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        databaseMonsterCards = gameManager.GetComponent<DatabaseMonsterCards>();
        player1 = gameManager.GetComponent<Player1>();
        player2 = gameManager.GetComponent<Player2>();
        draggable = gameObject.GetComponent<DraggableAttack>();

        MutationCard1.SetActive(false);
        MutationCard2.SetActive(false);
        MutationCard3.SetActive(false);
    }


    void Update()
    {
        CardDisplay();
        UppdateCardFacing();
        CalculateCost();
        IsCardBuyable(_cardPosition, playerManager.GetCurrentPlayerAction());
        CanCardAttack();
    }

    public virtual void ability()
    {

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


    public virtual void TryAttacking(GameObject enemy)
    {
        MonsterCard enemyCard = enemy.GetComponent<MonsterCard>();
        if (playerManager.MonsterCardInOtherHand(enemy))
        {
            Attack(enemy);
        }
        if (playerManager.MonsterCardInCurrentPlayerHand(enemy) && canHealAllies)
        {
            if (enemyCard.cantBeHealed == false)
            {
                HealAlly(enemy);
            }
        }
    }
    public virtual void Attack(GameObject enemy)
    {
        MonsterCard enemyCard = enemy.GetComponent<MonsterCard>();
        if (!cantBeHealed)
        {
            currentHp += healOnAttack;
        }
        enemyCard.TakeDamage(currentAttack, _statusEffectApplies, appliesTimer);
        if (attacksLeft <= 0)
        {
            canAttack = false;
            _cardAction = CardAction.Neutral;
            playerManager.CardDoneAttacking();
        }
        else if (attacksLeft > 0)
        {
            attacksLeft--;
        }
    }

    public virtual void TakeDamage(float damage, StatusEffectStates statusEffects, int statusEffectTimer)
    {

        currentHp -= DamageAfterModifiers(damage);
        _statusEffectAffected = statusEffects;
        affectedTimer += statusEffectTimer;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public virtual void HealAlly(GameObject enemy)
    {
        MonsterCard enemyCard = enemy.GetComponent<MonsterCard>();
        enemyCard.currentHp += currentAttack / 2;
        Mathf.Ceil(enemyCard.currentHp);
        canAttack = false;
        _cardAction = CardAction.Neutral;
        playerManager.CardDoneAttacking();

    }
    public virtual void ApplyStatusEffectsEndofTurn()
    {
        cantBeHealed = false;
        if (affectedTimer > 0)
        {
            if (_statusEffectAffected == StatusEffectStates.Poisioned)
            {
                currentHp -= affectedTimer;
                cantBeHealed = true;
            }
            affectedTimer--;
        }

        if (affectedTimer <= 0)
        {
            _statusEffectAffected = StatusEffectStates.None;
        }
    }

    public virtual float DamageAfterModifiers(float damage)
    {
        float damageAfterModifiers = damage;
        damageAfterModifiers -= flatDamageReduction;
        damageAfterModifiers *= (1 - percentageDamageReduction);
        if (_statusEffectAffected == StatusEffectStates.Weak)
        {
            damageAfterModifiers += 2;
        }

        return damageAfterModifiers;
    }

    public void UppdateCardEndOfTurn()
    {
        attacksLeft = howManyAttacks;
        ApplyStatusEffectsEndofTurn();
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


    public virtual void UppdateMutationCards()
    {
        for (int i = 0; i < MutationCards.Count; i++)
        {
            MutationCard mutationCard = MutationCards[i].GetComponent<MutationCard>();
            mutationCard._cardPosition = MutationCardPosition.onMonsterCard;

            mutationCard.transform.SetParent(transform);
            mutationCard.Hide();

            mutationCardEffect = mutationCard.GetComponent<MutationCardEffect>();
            mutationCardEffect.CheckState();
        }

        if (MutationCards.Count >= 1)
        {
            MutationCard1.SetActive(true);
            MutationCard mutation = MutationCards[0].GetComponent<MutationCard>();
            Mutationcard1NameText.text = mutation.cardName;
        }
        else MutationCard1.SetActive(false);

        if (MutationCards.Count >= 2)
        {
            MutationCard2.SetActive(true);
            MutationCard mutation = MutationCards[1].GetComponent<MutationCard>();
            Mutationcard2NameText.text = mutation.cardName;
        }
        else MutationCard2.SetActive(false);

        if (MutationCards.Count >= 3)
        {
            MutationCard3.SetActive(true);
            MutationCard mutation = MutationCards[2].GetComponent<MutationCard>();
            Mutationcard3NameText.text = mutation.cardName;
        }
        else MutationCard3.SetActive(false);
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
    public virtual void Die()
    {
        if (willRevive)
        {
            Revive();
        }
        else
        {
            playerManager.AddDeadCard(this);
        }
    }
    public virtual void Revive()
    {
        currentHp = hpAfterRevive;
        currentAttack = attackAfterRevive;
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
        if (cardPosition == CardPosition.inStore && cost <= playerCurrency && cardsOnHand < 4)  //l?gg till cost
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
    }
}
