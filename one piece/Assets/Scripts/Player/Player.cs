using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerState State { get; set; }

    public Transform handTransform, deckTransform, fieldTransform, cemeteryTransform;
    public Text lifeText, manaText, playerNameText;

    public Image portrait;
    public int Life { get; set; }
    public int Mana { get; set; }
    public bool CanAttack { get; set; }
    public bool CanBeAttacked { get; set; }
    public bool CanUseEffect { get; set; }

    private Deck deck;
    private List<Card> deckCards = new List<Card>();
    private List<Card> handCards = new List<Card>();
    public List<Card> fieldCards = new List<Card>();

    private List<Card> cemeteryCards = new List<Card>();

    public string PlayerName { get; set; }

    public int maxNumFieldCards = 6;

    public void InitiatePlayer()
    {
        deck = ScriptableObject.CreateInstance<Deck>();
        deck.deckCards = stats.InitialDeck.deckCards;
        PlayerName = stats.playerName;
        Life = stats.initialLife;
        Mana = stats.initialMana;
        CanAttack = true;
        State = stats.normalState;
        portrait.sprite = stats.portrait;
        UpdateStats();
    }

    public void UpdateStats()
    {
        // ✅ Bloque le mana et la vie à un minimum de 0
        if (Mana < 0) Mana = 0;
        if (Life < 0) Life = 0;

        lifeText.text = Life.ToString();
        manaText.text = Mana.ToString();
        playerNameText.text = PlayerName;

        Debug.Log(PlayerName + " a " + Life + " PV et " + Mana + " mana disponible.");
    }

    public void CheckGameOver()
    {
        if (Life <= 0)
        {
            Life = 0; // ✅ Assure que la vie reste à 0
            UpdateStats(); // ✅ Met à jour immédiatement l'affichage

            Debug.Log(PlayerName + " a perdu !");
            
            // ✅ Affichage du gagnant
            string winner = (this == Settings.main.player1) ? Settings.main.player2.PlayerName : Settings.main.player1.PlayerName;
            Debug.Log("🏆 " + winner + " a gagné la partie !");
            
            Settings.main.GameOver(this); // ✅ Stoppe immédiatement le jeu
        }
    }

    public void SpendMana(int amount)
    {
        if (Mana >= amount)
        {
            Mana -= amount;
        }
        else
        {
            Mana = 0; // ✅ Empêche d'avoir un mana négatif
        }
        UpdateStats();
    }

    public void CreateDeck()
    {
        if (deck != null)
        {
            deck.Shuffle();
            for (int i = 0; i < deck.deckCards.Count; i++)
            {
                CreateCardToDeck(deck.deckCards[i]);
            }
        }
    }

    public void CreateCardToDeck(string cardName)
    {
        GameObject objectCard = Instantiate(Settings.main.prefabCard, deckTransform);
        Card card = objectCard.GetComponent<Card>();
        card.cardBackground.SetActive(true);
        card.CreateCard(Settings.main.Rm.GetCardByName(cardName));
        card.CurrentLogic = stats.deckCardLogic;
        card.OwnerPlayer = this;
        deckCards.Add(card);
    }

    public void DrawInitialCards()
    {
        for (int i = 0; i < 5; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (deckCards.Count > 0 && handCards.Count < Settings.main.maxNumHandCards)
        {
            Card pulledCard = deckCards[deckCards.Count - 1];
            pulledCard.transform.SetParent(handTransform);
            pulledCard.cardBackground.SetActive(false);
            handCards.Add(pulledCard);
            deckCards.Remove(pulledCard);
            pulledCard.CurrentLogic = stats.handCardLogic;
        }
    }

    public void DropCard(Card card)
    {
        if (fieldCards.Count < maxNumFieldCards && Mana >= card.Cost)
        {
            card.transform.SetParent(fieldTransform);
            fieldCards.Add(card);
            handCards.Remove(card);
            card.CurrentLogic = stats.fieldCardLogic;
            Mana -= card.Cost;
            UpdateStats();
        }
    }

    public void DropRandomCard()
    {
        while (handCards.Count > 0 && fieldCards.Count < maxNumFieldCards)
        {
            List<Card> dropableCards = new List<Card>();
            foreach (Card c in handCards)
            {
                if (c.Cost <= Mana) 
                {
                    dropableCards.Add(c);
                }
            }

            if (dropableCards.Count == 0) break;

            Card card = dropableCards[Random.Range(0, dropableCards.Count)];
            DropCard(card);
        }
    }

    public void KillCard(Card card)
    {
        if (fieldCards.Contains(card))
        {
            card.transform.SetParent(cemeteryTransform);
            cemeteryCards.Add(card);
            fieldCards.Remove(card);
            card.CurrentLogic = stats.cemeteryCardLogic;
            card.CreateCard(card.Stats);
        }
    }

    public void Attack(Card attackingCard, Card targetCard)
    {
        int attackingCardPower = attackingCard.Attack;
        int targetCardPower = targetCard.Attack;

        targetCard.Attack -= attackingCardPower;
        attackingCard.Attack -= targetCardPower;

        targetCard.UpdateCardStats();
        attackingCard.UpdateCardStats();

        if (targetCard.Attack <= 0) 
        {
            targetCard.OwnerPlayer.Life -= attackingCardPower;
            targetCard.OwnerPlayer.CheckGameOver();
            KillCard(targetCard);
        }

        if (attackingCard.Attack <= 0) 
        {
            attackingCard.OwnerPlayer.Life -= targetCardPower;
            attackingCard.OwnerPlayer.CheckGameOver();
            KillCard(attackingCard);
        }

        targetCard.OwnerPlayer.UpdateStats();
        attackingCard.OwnerPlayer.UpdateStats();
    }

    public void Attack(Card attackingCard, Player targetPlayer)
    {
        if (targetPlayer.CanBeAttacked)
        {
            targetPlayer.Life -= attackingCard.Attack;
            targetPlayer.CheckGameOver();
        }
    }
}
