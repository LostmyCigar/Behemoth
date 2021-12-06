using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Ceasar : MonoBehaviour
{
    public int attackForRemoving;

    public MonsterCard monsterCard;
    public GameObject gameManager;
    public PlayerManager playerManager;
    private GameObject mutationCard;
    private MutationCard mutationCardScript;
    private DatabaseMutationCards databaseMutationCards;
    private MutationCardEffect mutationCardEffect;

    void Start()
    {
        monsterCard = GetComponent<MonsterCard>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        databaseMutationCards = gameManager.GetComponent<DatabaseMutationCards>();
    }

    public void RemoveCard1()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(gameObject))
        {
            mutationCard = monsterCard.MutationCards[0];
            mutationCardScript = mutationCard.GetComponentInChildren<MutationCard>();
            mutationCardEffect = mutationCard.GetComponentInChildren<MutationCardEffect>();
            mutationCardEffect.Deactivate();


            monsterCard.MutationCards.Remove(monsterCard.MutationCards[0]);
            monsterCard.currentAttack += attackForRemoving;
            monsterCard.currentHp += mutationCardScript.cardCost;
            monsterCard.currentMutationSpace++;


            monsterCard.UppdateMutationCards();
            Destroy(mutationCard);
        }

    }

    public void RemoveCard2()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(gameObject))
        {

            mutationCard = monsterCard.MutationCards[1];
            mutationCardScript = mutationCard.GetComponentInChildren<MutationCard>();
            mutationCardEffect = mutationCard.GetComponentInChildren<MutationCardEffect>();
            mutationCardEffect.Deactivate();


            monsterCard.MutationCards.Remove(monsterCard.MutationCards[1]);
            monsterCard.currentAttack += attackForRemoving;
            monsterCard.currentHp += mutationCardScript.cardCost;
            monsterCard.currentMutationSpace++;



            monsterCard.UppdateMutationCards();
            Destroy(mutationCard);
        }
    }

    public void RemoveCard3()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(gameObject))
        {
            mutationCard = monsterCard.MutationCards[2];
            mutationCardScript = mutationCard.GetComponentInChildren<MutationCard>();
            mutationCardEffect = mutationCard.GetComponentInChildren<MutationCardEffect>();
            mutationCardEffect.Deactivate();


            monsterCard.MutationCards.Remove(monsterCard.MutationCards[2]);
            monsterCard.currentAttack += attackForRemoving;
            monsterCard.currentHp += mutationCardScript.cardCost;
            monsterCard.currentMutationSpace++;


            monsterCard.UppdateMutationCards();
            Destroy(mutationCard);
        }
    }
}
