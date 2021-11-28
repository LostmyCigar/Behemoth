using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseMonsterCards : MonoBehaviour
{
 
    public List<GameObject> MonsterCards = new List<GameObject>();
    public List<GameObject> MonsterCardDeck = new List<GameObject>();
    public List<GameObject> MonsterCardStore = new List<GameObject>();
    public GameObject tempGO;
    public MonsterCard monsterCard;


    private void Start()
    {
        CreateMonsterDeck();
        ShuffleMonsterDeck();
        CreateMonsterStore();
        AddCardsToStore();
        FacedownDeck();
    }

    private void CreateMonsterDeck()
    {
        MonsterCardDeck = new List<GameObject>();
        for (int i = 0; i < MonsterCards.Count; i++)
        {
            MonsterCardDeck.Add(MonsterCards[i]);
            monsterCard = MonsterCardDeck[i].GetComponent<MonsterCard>();
            monsterCard._cardPosition = CardPosition.inDeck;
        }
    }
    private void ShuffleMonsterDeck()
    {
        for (int i = 0; i < MonsterCardDeck.Count - 1; i++)
        {
            int rnd = Random.Range(i, MonsterCardDeck.Count);
            tempGO = MonsterCardDeck[rnd];
            MonsterCardDeck[rnd] = MonsterCardDeck[i];
            MonsterCardDeck[i] = tempGO;
        }
    }
 
    private void CreateMonsterStore()
    {

        MonsterCardStore = new List<GameObject>();
        int i = 0;
        while (MonsterCardStore.Count < 3)
        {
            MonsterCardStore.Add(MonsterCardDeck[i]);
            GameObject currentCard = MonsterCardDeck[i];
            MonsterCardDeck.Remove(currentCard);
            i++;
        }

    }
    private void AddCardsToStore()
    {
        for (int i = 0; i < MonsterCardStore.Count; i++)
        {
            monsterCard = MonsterCardStore[i].GetComponent<MonsterCard>();
            monsterCard._cardPosition = CardPosition.inStore;
            monsterCard.FaceUp();         


            GameObject thisCard = Instantiate(MonsterCardStore[i], Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("MonsterCardStore").transform);
            MonsterCardStore.Remove(MonsterCardStore[i]);
            MonsterCardStore.Insert(i, thisCard);
        }
    }

    public void NewCardToStore()
    {
        if (MonsterCardStore.Count < 3)
        {
            MonsterCardStore.Add(MonsterCardDeck[0]);
            MonsterCardDeck.Remove(MonsterCardDeck[0]);

            monsterCard = MonsterCardStore[2].GetComponent<MonsterCard>();
            monsterCard._cardPosition = CardPosition.inStore;
            monsterCard.FaceUp();
            GameObject thisCard = Instantiate(MonsterCardStore[2], Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("MonsterCardStore").transform);
            MonsterCardStore.Remove(MonsterCardStore[2]);
            MonsterCardStore.Insert(2, thisCard);
        }
    }


    private void FacedownDeck()
    {
        for (int i = 0; i < MonsterCardDeck.Count; i++)
        {
            monsterCard = MonsterCardDeck[i].GetComponent<MonsterCard>();
            monsterCard.FaceDown();
        }
    }
}
