using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseWeaponCards : MonoBehaviour
{
    public List<GameObject> WeaponCards = new List<GameObject>();
    public List<GameObject> WeaponCardDeck = new List<GameObject>();
    public List<GameObject> WeaponCardStore = new List<GameObject>();
    public GameObject tempGO;
    public WeaponCard weaponCard;
    void Start()
    {
        CreateWeaponDeck();
        ShuffleWeaponDeck();
        CreateWeaponStore();
        AddCardsToStore();
    }

    private void CreateWeaponDeck()
    {
        WeaponCardDeck = new List<GameObject>();
        for (int i = 0; i < WeaponCards.Count; i++)
        {
            WeaponCardDeck.Add(WeaponCards[i]);
            weaponCard = WeaponCardDeck[i].GetComponent<WeaponCard>();
            weaponCard._cardPosition = CardPosition.inDeck;
        }
    }
    private void ShuffleWeaponDeck()
    {
        for (int i = 0; i < WeaponCardDeck.Count - 1; i++)
        {
            int rnd = Random.Range(i, WeaponCardDeck.Count);
            tempGO = WeaponCardDeck[rnd];
            WeaponCardDeck[rnd] = WeaponCardDeck[i];
            WeaponCardDeck[i] = tempGO;
        }
    }

    private void CreateWeaponStore()
    {

        WeaponCardStore = new List<GameObject>();
        int i = 0;
        while (WeaponCardStore.Count < 4)
        {
            WeaponCardStore.Add(WeaponCardDeck[i]);
            GameObject currentCard = WeaponCardDeck[i];
            WeaponCardDeck.Remove(currentCard);
            i++;
        }

    }
    private void AddCardsToStore()
    {
        for (int i = 0; i < WeaponCardStore.Count; i++)
        {
            weaponCard = WeaponCardStore[i].GetComponent<WeaponCard>();
            weaponCard._cardPosition = CardPosition.inStore;


            GameObject thisCard = Instantiate(WeaponCardStore[i], Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("WeaponCardStore").transform);
            WeaponCardStore.Remove(WeaponCardStore[i]);
            WeaponCardStore.Insert(i, thisCard);
        }
    }

    public void NewCardToStore()
    {
        if (WeaponCardDeck.Count > 0)
        {
            if (WeaponCardStore.Count < 4)
            {
                WeaponCardStore.Add(WeaponCardDeck[0]);
                WeaponCardDeck.Remove(WeaponCardDeck[0]);

                weaponCard = WeaponCardStore[3].GetComponent<WeaponCard>();
                weaponCard._cardPosition = CardPosition.inStore;
                GameObject thisCard = Instantiate(WeaponCardStore[3], Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("WeaponCardStore").transform);
                WeaponCardStore.Remove(WeaponCardStore[3]);
                WeaponCardStore.Insert(3, thisCard);
            }
        }
    }

}
