using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cacrow : MonsterCard
{

    public int currentBonus;



    private void CheckAbility()
    {
        if (MutationCards.Count > currentBonus)
        {
            ability();
        }
    }

    public override void UppdateMutationCards()
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
        CheckAbility();
    }
    public override void ability()
    {
        currentAttack++;
        currentBonus++;
    }
}
