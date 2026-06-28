using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Battle
{
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        Busy,
        Win,
        Lose
    }

    public enum BattleAction
    {
        None,
        Attack,
        Defend,
        Heal
    }

    public enum EnemyAction
    {
        Attack,
        Defend,
        Heal
    }
}

