using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject prefabCard;
    public Player currentPlayer, player1, player2;
    public int maxNumHandCards = 7; // ✅ Nombre max de cartes en main
    public int maxNumFieldCards = 6; // ✅ Nombre max de cartes sur le terrain

    public Text currentStateText;
    public Text currentPlayerText;
    ResourcesManager rm;
    public ResourcesManager Rm { get => rm; set => rm = value; }

    void OnEnable()
    {
        Settings.main = this;
        Rm = Settings.GetResourcesManager();

        player1.InitiatePlayer();
        player1.CreateDeck();
        player1.DrawInitialCards();
        player1.maxNumFieldCards = maxNumFieldCards; // ✅ Assurer que les deux ont la même limite

        player2.InitiatePlayer();
        player2.CreateDeck();
        player2.DrawInitialCards();
        player2.maxNumFieldCards = maxNumFieldCards; // ✅ Assurer que les deux ont la même limite

        UpdateUI();
    }

    public void ChangePlayer()
    {
        currentPlayer.StopAllCoroutines();
        currentPlayer.State = currentPlayer.stats.normalState;

        if (currentPlayer == player2)
        {
            currentPlayer = player1;
        }
        else
        {
            currentPlayer = player2;
        }

        currentPlayer.StopAllCoroutines();
        currentPlayer.State = currentPlayer.stats.normalState;
        UpdateUI();
    }

    public void UpdateUI()
    {
        currentPlayerText.text = currentPlayer.PlayerName;
        currentStateText.text = currentPlayer.State.name;
    }

    public void GameOver(Player loser)
    {
        Debug.Log(loser.PlayerName + " a perdu !");
    }
}
