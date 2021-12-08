using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public enum PlayerTurnState
{
    StartPlayer1,
    StartPlayer2,
    Player1,
    Player2,
}
public class PlayerManager : MonoBehaviour
{
    public string playerTurnString;
    public string playerActionString;
    public Text playerTurnText;
    public Text playerActionText;
    public PlayerTurnState _playerTurnState;
    public Player1 player1;
    public Player2 player2;
    public Button endTurnButton;

    void Start()
    {
        _playerTurnState = PlayerTurnState.StartPlayer1;
        player1 = GetComponent<Player1>();
        player2 = GetComponent<Player2>();
        endTurnButton.onClick.AddListener(delegate { EndTurn(false); });
    }

    
    void Update()
    {
        UppdatePlayerTurn();
        UppdateTurnText();
        EndTurnButtonOnState();
    }

    private void UppdatePlayerTurn()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            player1.enabled = true;
            player2.enabled = false;

        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            player1.enabled = false;
            player2.enabled = true;

        }
    }

    private PlayerTurnState GetState()
    {
        return _playerTurnState;
    }
    private void SetState(PlayerTurnState state)
    {
        _playerTurnState = state;
    }

    public List<GameObject> GetAllActiveMonsterCards()
    {
         List<GameObject> AllMonsterCards = new List<GameObject>();
         AllMonsterCards = GameObject.FindGameObjectsWithTag("MonsterCard").ToList();

        return AllMonsterCards;
    }

    public List<GameObject> GetAllActiveWeaponCards()
    {
        List<GameObject> AllWeaponCards = new List<GameObject>();
        AllWeaponCards = GameObject.FindGameObjectsWithTag("WeaponCard").ToList();

        return AllWeaponCards;
    }

    private void UppdateTurnText()
    {
        if (_playerTurnState == PlayerTurnState.StartPlayer1)
        {
            playerTurnString = "Player 1";
            playerActionString = "Choose starting card";
        }
        else if (_playerTurnState == PlayerTurnState.StartPlayer2)
        {
            playerTurnString = "Player 2";
            playerActionString = "Choose starting card";
        }
        else if (_playerTurnState == PlayerTurnState.Player1)
        {
            playerTurnString = "Player 1 Turn";
            if (GetCurrentPlayerAction() == PlayerAction.ChooseAction)
            {
                playerActionString = "Choose Action";
            }
            else if (GetCurrentPlayerAction() == PlayerAction.AttackPhase)
            {
                playerActionString = "Attack";
            }
            else if (GetCurrentPlayerAction() == PlayerAction.BuyPhase)
            {
                playerActionString = "Buy Cards or End turn";
            }
        }
        else if (_playerTurnState == PlayerTurnState.Player2)
        {
            playerTurnString = "Player 2 Turn";
            if (GetCurrentPlayerAction() == PlayerAction.ChooseAction)
            {
                playerActionString = "Choose Action";
            }
            else if (GetCurrentPlayerAction() == PlayerAction.AttackPhase)
            {
                playerActionString = "Attack";
            }
            else if (GetCurrentPlayerAction() == PlayerAction.BuyPhase)
            {
                playerActionString = "Buy Cards or End turn";
            }
        }
        playerTurnText.text = playerTurnString;
        playerActionText.text = playerActionString;

    }

    public int GetCurrentPlayerRecruitmentPoints()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            return player1.recruitmentPoints;
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            return player2.recruitmentPoints;
        }

        else return 0;
    }

    public int GetCurrentPlayerMutationPoints()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            return player1.mutationPoints;
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            return player2.mutationPoints;
        }

        else return 0;
    }

    public void SetCurrentPlayerMutationPoints(int newValue)
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            player1.mutationPoints = newValue;
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            player2.mutationPoints = newValue;
        }
    }

    public PlayerAction GetCurrentPlayerAction()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            return player1._playerAction;
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            return player2._playerAction;
        }
        else return 0;
    }

    public void SetCurrentPlayerAction(PlayerAction playerAction)
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            player1._playerAction = playerAction;
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            player2._playerAction = playerAction;
        }
    }

    public int GetCurrentPlayerCardsOnHand()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            return player1.currentCardsOnHand;
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            return player2.currentCardsOnHand;
        }
        else return 0;
    }

    public MonoBehaviour GetCurrentPlayer()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            return player1;
        }
        else 
        {
            return player2;
        }
    }

    public void GivePlayerMutationPoints(int mutationPoints)
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            player1.mutationPoints += mutationPoints;
        }
        else
        {
            player2.mutationPoints += mutationPoints;
        }
    }

    public void GivePlayerRecruitmentPoints(int recruitmentPoints)
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            player1.recruitmentPoints += recruitmentPoints;
        }
        else
        {
            player2.recruitmentPoints += recruitmentPoints;
        }
    }

    public void GivePlayerAttackPoints(int attackPoints)
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            player1.attacksAvailable += attackPoints;
            player1.Attack();

        }
        else
        {
            player2.attacksAvailable += attackPoints;
            player2.Attack();
        }
    }

    public void CardDoneAttacking()
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            player1.cardsDoneAttacking++;
        }
        else
        {
            player2.cardsDoneAttacking++;
        }
    }

    public void AddDeadCard(MonsterCard deadCard)
    {
        if (deadCard._cardPosition == CardPosition.inHandPlayer1)
        {
            player1.deadCards++;
            player1.Player1MonsterCards.Remove(deadCard.gameObject);
            if (player1.deadCards <= 4)
            {

            }
        }
        else if (deadCard._cardPosition == CardPosition.inHandPlayer2)
        {
            player2.deadCards++;
            player2.Player2MonsterCards.Remove(deadCard.gameObject);
            if (player2.deadCards <= 4)
            {
                
            }
        }
        Destroy(deadCard.gameObject);
    }
    public bool MonsterCardInOtherHand(GameObject card)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            if (monsterCard._cardPosition == CardPosition.inHandPlayer2)
            {
                return true;
            }
            else return false;
        }
        else
        {
            if (monsterCard._cardPosition == CardPosition.inHandPlayer1)
            {
                return true;
            }
            else return false;
        }
    }

    public bool MonsterCardInCurrentPlayerHand(GameObject card)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            if (monsterCard._cardPosition == CardPosition.inHandPlayer1)
            {
                return true;
            }
            else return false;
        }
        else
        {
            if (monsterCard._cardPosition == CardPosition.inHandPlayer2)
            {
                return true;
            }
            else return false;
        }
    }
    public bool WeaponCardInCurrentPlayerHand(GameObject card)
    {
        WeaponCard weaponCard = card.GetComponent<WeaponCard>();
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            if (weaponCard._cardPosition == CardPosition.inHandPlayer1)
            {
                return true;
            }
            else return false;
        }
        else
        {
            if (weaponCard._cardPosition == CardPosition.inHandPlayer2)
            {
                return true;
            }
            else return false;
        }
    }

    public void MovePlayerToNextPhase()
    {
        PlayerAction playerAction = GetCurrentPlayerAction();
        if (playerAction == PlayerAction.ChooseFirstCard)
        {
            SetCurrentPlayerAction(PlayerAction.ChooseAction);
        }
        else if (playerAction == PlayerAction.ChooseAction)
        {
            SetCurrentPlayerAction(PlayerAction.BuyPhase);
        }
        else if (playerAction == PlayerAction.BuyPhase)
        {
            SetCurrentPlayerAction(PlayerAction.ChooseAction);
        }
        else if (playerAction == PlayerAction.AttackPhase)
        {
            SetCurrentPlayerAction(PlayerAction.BuyPhase);
        }
    }

    public void UppdateMonsterCardsEndofTurn()
    {
        List<GameObject> AllMonsterCards = new List<GameObject>();
        AllMonsterCards = GetAllActiveMonsterCards();
        for (int i = 0; i < AllMonsterCards.Count; i++)
        {
            MonsterCard monsterCard = AllMonsterCards[i].GetComponent<MonsterCard>();
            monsterCard.UppdateCardEndOfTurn();
        }
    }

    public void UppdateWeaponCardsEndofTurn()
    {
        List<GameObject> AllWeaponCards = new List<GameObject>();
        AllWeaponCards = GetAllActiveWeaponCards();
        for (int i = 0; i < AllWeaponCards.Count; i++)
        {
            WeaponCard weaponCard = AllWeaponCards[i].GetComponent<WeaponCard>();
            if (weaponCard._cardPosition == CardPosition.inHandPlayer1)
            {
                weaponCard.UppdateCardEndOfTurn();
                Debug.Log("card uppdated");
            }
            if (weaponCard._cardPosition == CardPosition.inHandPlayer2)
            {
                weaponCard.UppdateCardEndOfTurn();
            }
        }
    }

    private void EndTurnButtonOnState()
    {
        if (GetCurrentPlayerAction() == PlayerAction.BuyPhase)
        {
            endTurnButton.interactable = true;
        }
        else
        {
            endTurnButton.interactable = false;
        }
    }
    public void EndTurn(bool firstTurn)
    {
        if (_playerTurnState == PlayerTurnState.Player1 || _playerTurnState == PlayerTurnState.StartPlayer1)
        {
            if (firstTurn)
            {
                SetCurrentPlayerAction(PlayerAction.ChooseAction);
                _playerTurnState = PlayerTurnState.StartPlayer2;
            }
            else
            {
                SetCurrentPlayerAction(PlayerAction.ChooseAction);
                _playerTurnState = PlayerTurnState.Player2;
                UppdateMonsterCardsEndofTurn();
                UppdateWeaponCardsEndofTurn();
            }
        }
        else if (_playerTurnState == PlayerTurnState.Player2 || _playerTurnState == PlayerTurnState.StartPlayer2)
        {
            if (firstTurn)
            {
                SetCurrentPlayerAction(PlayerAction.ChooseAction);
                _playerTurnState = PlayerTurnState.Player2;
            }
            else
            {
                SetCurrentPlayerAction(PlayerAction.ChooseAction);
                _playerTurnState = PlayerTurnState.Player1;
                UppdateMonsterCardsEndofTurn();
                UppdateWeaponCardsEndofTurn();
            }
        }
    }
}
