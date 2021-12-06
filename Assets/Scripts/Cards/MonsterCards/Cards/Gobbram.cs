using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Gobbram : MonoBehaviour
{
    public MonsterCard monsterCard;
    public GameObject gameManager;
    public Player1 player1;
    public Player2 player2;
    public PlayerManager playerManager;
    public bool notActivated;
    public GameObject MutationCard4;
    public Text Mutationcard4NameText;
    void Start()
    {
        monsterCard = GetComponent<MonsterCard>();
        notActivated = true;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        player1 = gameManager.GetComponent<Player1>();
        player2 = gameManager.GetComponent<Player2>();
        playerManager = gameManager.GetComponent<PlayerManager>();
    }


    void Update()
    {
        CheckAbility();
        if (monsterCard.MutationCards.Count >= 4)
        {
                MutationCard4.SetActive(true);
                MutationCard mutation = monsterCard.MutationCards[3].GetComponent<MutationCard>();
                Mutationcard4NameText.text = mutation.cardName;
        }
        else MutationCard4.SetActive(false);
    }

    private void CheckAbility()
    {
        if (monsterCard.canAttack && notActivated)
        {
            ability();
            notActivated = false;
        }
        else if (playerManager.MonsterCardInCurrentPlayerHand(this.gameObject) == false)
        {
            notActivated = true;
        }
    }

    private void ability()
    {
        if (monsterCard._cardPosition == CardPosition.inHandPlayer1)
        {
            player1.mutationPoints++;
        }
        else if (monsterCard._cardPosition == CardPosition.inHandPlayer2)
        {
            player2.mutationPoints++;
        }
    }
}
