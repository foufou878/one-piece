using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Card attackingCard;
    Card targetCard; // ✅ Correction ici

    public Card AttackingCard { get => attackingCard; set => attackingCard = value; }
    public Card TargetCard { get => targetCard; set => targetCard = value; }

    public void DoAttack()
    {
        if (AttackingCard != null && TargetCard != null)
        {
            TargetCard.Attack -= AttackingCard.Attack; // ✅ Correction ici
            AttackingCard.Attack -= TargetCard.Attack; // ✅ Correction ici

            TargetCard.UpdateCardStats();
            AttackingCard.UpdateCardStats();

            if (TargetCard.Attack <= 0) // ✅ Correction ici
                TargetCard.OwnerPlayer.KillCard(TargetCard);
            if (AttackingCard.Attack <= 0) // ✅ Correction ici
                AttackingCard.OwnerPlayer.KillCard(AttackingCard);
        }
    }
}
