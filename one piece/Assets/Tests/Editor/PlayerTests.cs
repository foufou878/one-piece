using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerTests
{
    private Player player1;
    private Player player2;

    [SetUp] // ✅ Exécuté avant chaque test
    public void Setup()
    {
        GameObject playerObj1 = new GameObject();
        GameObject playerObj2 = new GameObject();
        
        player1 = playerObj1.AddComponent<Player>();
        player2 = playerObj2.AddComponent<Player>();

        player1.Life = 20;
        player2.Life = 20;
        player1.Mana = 5;
        player2.Mana = 5;
    }

    [Test]
    public void Player_LosesLife_WhenAttacked()
    {
        player1.Life -= 5;
        Assert.AreEqual(15, player1.Life, "❌ La vie du joueur ne diminue pas correctement après une attaque !");
    }

    [Test]
    public void Player_CannotHaveNegativeLife()
    {
        player1.Life -= 25;
        player1.CheckGameOver();
        Assert.AreEqual(0, player1.Life, "❌ La vie du joueur ne devrait jamais être négative !");
    }

    [Test]
    public void Player_CannotSpendMoreManaThanAvailable()
    {
        player1.Mana -= 10;
        Assert.LessOrEqual(player1.Mana, 0, "❌ Le joueur ne peut pas avoir un mana négatif !");
    }

    [Test]
    public void Game_Ends_WhenPlayerHasZeroLife()
    {
        player1.Life = 0;
        player1.CheckGameOver();
        LogAssert.Expect(LogType.Log, player1.PlayerName + " a perdu !");
    }
}
