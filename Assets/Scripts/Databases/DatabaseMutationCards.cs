using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseMutationCards : MonoBehaviour
{
    public List<GameObject> MutationCards = new List<GameObject>();
    public List<GameObject> MutationCardDeck = new List<GameObject>();
    public List<GameObject> MutationCardStore = new List<GameObject>();
    public GameObject tempGO;
    public MutationCard mutationCard;
    void Start()
    {
        CreateMutationDeck();
        ShuffleMutationDeck();
        CreateMutationStore();
        AddCardsToStore();
        FacedownDeck();
    }

    private void CreateMutationDeck()
    {
        MutationCardDeck = new List<GameObject>();

        for (int i = 0; i < MutationCards.Count; i++)
        {
            MutationCardDeck.Add(MutationCards[i]);
            mutationCard = MutationCardDeck[i].GetComponent<MutationCard>();
            mutationCard._cardPosition = MutationCardPosition.inDeck;
        }
    }
    private void ShuffleMutationDeck()
    {
        for (int i = 0; i < MutationCardDeck.Count - 1; i++)
        {
            int rnd = Random.Range(i, MutationCardDeck.Count);
            tempGO = MutationCardDeck[rnd];
            MutationCardDeck[rnd] = MutationCardDeck[i];
            MutationCardDeck[i] = tempGO;
        }
    }

    private void CreateMutationStore()
    {

        MutationCardStore = new List<GameObject>();
        int i = 0;
        while (MutationCardStore.Count < 3)
        {
            MutationCardStore.Add(MutationCardDeck[i]);
            GameObject currentCard = MutationCardDeck[i];
            MutationCardDeck.Remove(currentCard);
            i++;
        }

    }
    private void AddCardsToStore()
    {
        for (int i = 0; i < MutationCardStore.Count; i++)
        {
            mutationCard = MutationCardStore[i].GetComponent<MutationCard>();
            mutationCard._cardPosition = MutationCardPosition.inStore;
            //mutationCard.FaceUp();


            GameObject thisCard = Instantiate(MutationCardStore[i], Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("MutationCardStore").transform);
            MutationCardStore.Remove(MutationCardStore[i]);
            MutationCardStore.Insert(i, thisCard);
        }
    }

    public void NewCardToStore()
    {
        if (MutationCardStore.Count < 3)
        {
            MutationCardStore.Add(MutationCardDeck[0]);
            MutationCardDeck.Remove(MutationCardDeck[0]);

            mutationCard = MutationCardStore[2].GetComponent<MutationCard>();
            mutationCard._cardPosition = MutationCardPosition.inStore;
          //  mutationCard.FaceUp();
            GameObject thisCard = Instantiate(MutationCardStore[2], Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("MutationCardStore").transform);
            MutationCardStore.Remove(MutationCardStore[2]);
            MutationCardStore.Insert(2, thisCard);
        }
    }


    private void FacedownDeck()
    {
        for (int i = 0; i < MutationCardDeck.Count; i++)
        {
            mutationCard = MutationCardDeck[i].GetComponent<MutationCard>();
         //   mutationCard.FaceDown();
        }
    }
}
