using Nitzz.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Battle
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [Header("Reference")]
        [SerializeField] private BattleUnit player;
        [SerializeField] private BattleUnit enemy;

        [SerializeField] private BattleUI battleUI;
        public BattleState State;

        public BattleUnit PlayerBattle => player;
        public BattleUnit EnemyBattle => enemy;

        [Header("Popup")]
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private GameObject resultPanelWin;
        [SerializeField] private GameObject resultPanelLose;

        [Header("Flow Text")]
        public DamageText damagePopupPrefab;
        public Transform parentFlowtext;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            State = BattleState.PlayerTurn;

            battleUI.UpdateHP(player, enemy);

            battleUI.SetEnemyAction("-");

            battleUI.EnablePlayerAction(true);
        }

        #region PLAYER
        public void PlayerAttack()
        {
            if (State != BattleState.PlayerTurn)
                return;

            StartCoroutine(PlayerAttackRoutine());
        }

        public void PlayerDefend()
        {
            if (State != BattleState.PlayerTurn)
                return;

            player.Defend();

            StartEnemyTurn();
        }

        public void PlayerHeal()
        {
            if (State != BattleState.PlayerTurn)
                return;

            player.Heal();

            battleUI.UpdateHP(player, enemy);

            StartEnemyTurn();
        }

        IEnumerator PlayerAttackRoutine()
        {
            State = BattleState.Busy;

            battleUI.EnablePlayerAction(false);

            player.PlayAttack(enemy.transform.position + Vector3.left * 1f, () =>
            {
                enemy.TakeDamage(player.attack);

                battleUI.UpdateHP(player, enemy);
            });

            yield return new WaitForSeconds(.55f);

            if (CheckBattleEnd())
                yield break;

            StartEnemyTurn();
        }
        #endregion PLAYER

        void StartEnemyTurn()
        {
            State = BattleState.EnemyTurn;

            StartCoroutine(EnemyTurnRoutine());
        }

        IEnumerator EnemyTurnRoutine()
        {
            yield return new WaitForSeconds(.5f);

            EnemyAction action = BattleAI.Decide(enemy);

            battleUI.SetEnemyAction(action.ToString());

            switch (action)
            {
                case EnemyAction.Attack:

                    enemy.PlayAttack(player.transform.position + Vector3.right * 1f, () =>
                    {
                        player.TakeDamage(enemy.attack);

                        battleUI.UpdateHP(player, enemy);
                    });

                    yield return new WaitForSeconds(.55f);

                    break;

                case EnemyAction.Defend:

                    enemy.Defend();

                    yield return new WaitForSeconds(.3f);

                    break;

                case EnemyAction.Heal:

                    enemy.Heal();

                    battleUI.UpdateHP(player, enemy);

                    yield return new WaitForSeconds(.3f);

                    break;
            }

            if (CheckBattleEnd())
                yield break;

            battleUI.SetEnemyAction("-");

            State = BattleState.PlayerTurn;

            battleUI.EnablePlayerAction(true);
        }

        bool CheckBattleEnd()
        {
            if (enemy.CurrentHP <= 0)
            {
                State = BattleState.Win;

                resultPanel.SetActive(true);
                resultPanelWin.SetActive(true);
                resultPanelLose.SetActive(false);

                return true;
            }

            if (player.CurrentHP <= 0)
            {
                State = BattleState.Lose;

                resultPanel.SetActive(true);
                resultPanelWin.SetActive(false);
                resultPanelLose.SetActive(true);

                return true;
            }

            return false;
        }

        public void BackMainMenu()
        {
            SceneTransition.Instance.FadeOut("MainMenu");
        }

    }
}