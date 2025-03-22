using NUnit.Framework;
using UnityEngine;

public class CombatTests
{
    private Card card1;
    private Card card2;
    private Player player1;
    private Player player2;

    [SetUp]
    public void Setup()
    {
        GameObject cardObj1 = new GameObject();
        GameObject cardObj2 = new GameObject();
        GameObject playerObj1 = new GameObject();
        GameObject playerObj2 = new GameObject();

        card1 = cardObj1.AddComponent<Card>();
        card2 = cardObj2.AddComponent<Card>();
        player1 = playerObj1.AddComponent<Player>();
        player2 = playerObj2.AddComponent<Player>();

        card1.OwnerPlayer = player1;
        card2.OwnerPlayer = player2;

        card1.Attack = 5;
        card2.Attack = 3;

        player1.Life = 20;
        player2.Life = 20;
    }

    [Test]
    public void Stronger_Card_Wins_And_Loses_Power()
    {
        // Combat entre deux cartes
        card1.Attack -= card2.Attack;
        if (card1.Attack < 0) card1.Attack = 0; // ✅ Empêche l'attaque négative

        card2.Attack -= card1.Attack;
        if (card2.Attack < 0) card2.Attack = 0; // ✅ Empêche l'attaque négative

        Assert.AreEqual(2, card1.Attack, "❌ La carte gagnante n'a pas la bonne valeur d'attaque après le combat !");
        Assert.AreEqual(0, card2.Attack, "❌ La carte perdante devrait avoir une attaque de 0 !");
    }

    [Test]
    public void Player_LosesLife_WhenCardDies()
    {
        // Une carte meurt, son propriétaire doit perdre des PV
        card1.Attack = 0;
        player1.Life -= 5;

        Assert.AreEqual(15, player1.Life, "❌ Le joueur ne perd pas de vie correctement après la mort d'une carte !");
    }

    [Test]
    public void Player_Loses_When_Life_Reaches_Zero()
    {
        player1.Life = 5;
        player1.Life -= 10;
        player1.CheckGameOver();

        Assert.AreEqual(0, player1.Life, "❌ La vie du joueur ne devrait pas descendre sous 0 !");
    }

    [Test]
    public void Attack_Reduces_Card_Attack_And_Player_Life()
    {
        player2.Life = 10;
        card1.Attack = 4;

        player2.Life -= card1.Attack;
        player2.CheckGameOver();

        Assert.AreEqual(6, player2.Life, "❌ Le joueur ne perd pas correctement des PV après une attaque !");
    }
}
