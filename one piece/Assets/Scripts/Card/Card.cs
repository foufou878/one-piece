using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject cardBackground;
    CardStats stats;
    CardType type;
    Sprite art;
    Logic currentLogic;
    Player ownerPlayer;
    string cardName;
    int cost;
    string effect;
    string flavorText;
    bool canAttack, canBeAttacked;

    public int Attack { get; set; } // ✅ Ajout correct de Attack

    public string FlavorText { get => flavorText; set => flavorText = value; }
    public string Effect { get => effect; set => effect = value; }
    public int Cost { get => cost; set => cost = value; }
    public string CardName { get => cardName; set => cardName = value; }
    public CardType Type { get => type; set => type = value; }
    public Sprite Art { get => art; set => art = value; }
    public CardStats Stats { get => stats; set => stats = value; }
    public Player OwnerPlayer { get => ownerPlayer; set => ownerPlayer = value; }
    public Logic CurrentLogic { get => currentLogic; set => currentLogic = value; }
    public Text textCardName, textPower, textCost, textEffect, textFlavor;
    public Image artImage, template;

    public void CreateCard(CardStats stats)
    {
        Stats = stats;
        CardName = Stats.cardName;
        Attack = Stats.power;
        Cost = Stats.cost;
        Effect = Stats.effect;
        FlavorText = Stats.flavorText;
        Type = Stats.type;
        Art = Stats.art;
        UpdateCardStats();
    }

    public void UpdateCardStats()
    {
        template.sprite = Stats.template;
        textCardName.text = cardName;
        textPower.text = Attack.ToString();
        if (type.hasPower)
        {
            textPower.gameObject.SetActive(true);
        }
        else
        {
            textPower.gameObject.SetActive(false);
        }
        textCost.text = cost.ToString();
        textEffect.text = effect;
        textFlavor.text = '"' + flavorText + '"';
        artImage.sprite = art;
    }

    public void CheckCardPower()
    {
        if (Attack <= 0)
            ownerPlayer.KillCard(this);
    }

    public void OnClick()
    {
        if (currentLogic != null)
            currentLogic.OnClick(this);
    }

    public void OnHover()
    {
        if (currentLogic != null)
            currentLogic.OnHover(this);
    }
}
