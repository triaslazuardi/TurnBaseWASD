using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Battle
{
    public static class BattleAI
    {
        public static EnemyAction Decide(BattleUnit enemy)
        {
            float hpPercent = (float)enemy.CurrentHP / enemy.maxHP;

            if (hpPercent < .35f)
            {
                int random = Random.Range(0, 100);

                if (random < 50)
                    return EnemyAction.Heal;

                if (random < 80)
                    return EnemyAction.Attack;

                return EnemyAction.Defend;
            }

            else
            {
                int random = Random.Range(0, 100);

                if (random < 70)
                    return EnemyAction.Attack;

                if (random < 90)
                    return EnemyAction.Defend;

                return EnemyAction.Heal;
            }
        }
    }
}