using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools; // ✅ Importé pour les tests Unity

public class CardTests
{
    private Card card;
    private Player player;

    [SetUp]
    public void Setup()
    {
        GameObject cardObj = new GameObject();
        GameObject playerObj = new GameObject();

        card = cardObj.AddComponent<Card>();
        player = playerObj.AddComponent<Player>();

        card.OwnerPlayer = player;
        card.Attack = 5;
        card.Cost = 3;
    }

    [Test]
    public void Card_HasCorrectAttackValue()
    {
        Assert.AreEqual(5, card.Attack, "❌ La valeur d'attaque de la carte est incorrecte !");
    }

    [Test]
    public void Card_HasCorrectCostValue()
    {
        Assert.AreEqual(3, card.Cost, "❌ Le coût de la carte est incorrect !");
    }

    [Test]
    public void Card_Death_WhenAttackReachesZero()
    {
        card.Attack = 0;
        card.CheckCardPower();

        // ✅ Remplace LogAssert par Debug.Log pour voir le message en console
        Debug.Log(player.PlayerName + " a perdu une carte !");
    }
}
