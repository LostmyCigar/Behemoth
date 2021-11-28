using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityClass : MonoBehaviour
{
    public List<GameObject> SelectedMonsterCards = new List<GameObject>();

    public void SelectMonsterCard(GameObject card)
    {

        for (int i = 0; i < SelectedMonsterCards.Count; i++)
        {
            MonsterCard monsterCard = SelectedMonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.isSelected = false; 

        }

        DeselectAllCards();
        SelectedMonsterCards.Add(card);


        for (int i = 0; i < SelectedMonsterCards.Count; i++)
        {
            MonsterCard monsterCard = SelectedMonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.isSelected = true;

        }
    }

    public void DeselectAllCards()
    {
        SelectedMonsterCards.Clear();
    }

    public List<GameObject> GetSelectedMonsterCards()
    {
        return SelectedMonsterCards;
    }
}
