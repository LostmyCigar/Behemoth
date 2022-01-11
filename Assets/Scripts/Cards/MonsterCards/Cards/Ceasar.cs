using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Ceasar : MonsterCard
{
    public int attackForRemoving;

    private GameObject mutationCard;
    private MutationCard mutationCardScript;
    //private DatabaseMutationCards databaseMutationCards;
    private MutationCardEffect mutationCardEffectScript;

    void Start()
    {

        //databaseMutationCards = gameManager.GetComponent<DatabaseMutationCards>();
    }

    public void RemoveCard1()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(gameObject))
        {
            mutationCard = MutationCards[0];
            mutationCardScript = mutationCard.GetComponentInChildren<MutationCard>();
            mutationCardEffectScript = mutationCard.GetComponentInChildren<MutationCardEffect>();
            mutationCardEffectScript.Deactivate();


            MutationCards.Remove(MutationCards[0]);
            currentAttack += attackForRemoving;
            currentHp += mutationCardScript.cardCost;
            currentMutationSpace++;


            UppdateMutationCards();
            Destroy(mutationCard);
        }

    }

    public void RemoveCard2()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(gameObject))
        {

            mutationCard = MutationCards[1];
            mutationCardScript = mutationCard.GetComponentInChildren<MutationCard>();
            mutationCardEffectScript = mutationCard.GetComponentInChildren<MutationCardEffect>();
            mutationCardEffectScript.Deactivate();


            MutationCards.Remove(MutationCards[1]);
            currentAttack += attackForRemoving;
            currentHp += mutationCardScript.cardCost;
            currentMutationSpace++;



            UppdateMutationCards();
            Destroy(mutationCard);
        }
    }

    public void RemoveCard3()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(gameObject))
        {
            mutationCard = MutationCards[2];
            mutationCardScript = mutationCard.GetComponentInChildren<MutationCard>();
            mutationCardEffectScript = mutationCard.GetComponentInChildren<MutationCardEffect>();
            mutationCardEffectScript.Deactivate();


            MutationCards.Remove(MutationCards[2]);
            currentAttack += attackForRemoving;
            currentHp += mutationCardScript.cardCost;
            currentMutationSpace++;


            UppdateMutationCards();
            Destroy(mutationCard);
        }
    }
}
