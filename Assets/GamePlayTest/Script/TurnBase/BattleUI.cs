using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nitzz.Battle
{
    public class BattleUI : MonoBehaviour
    {
        [Header("Player")]
        public Image playerHP;
        public TMP_Text playerHPText;

        [Header("Enemy")]
        public Image enemyHP;
        public TMP_Text enemyHPText;

        [Header("Enemy Action")]
        public TMP_Text enemyActionText;

        [Space, Header("Button Action")]
        [SerializeField] private Button attackButton;
        [SerializeField] private Button defendButton;
        [SerializeField] private Button healButton;

        public void UpdateHP(BattleUnit player, BattleUnit enemy)
        {
            playerHP.DOFillAmount((float)player.CurrentHP / player.maxHP, 0.25f);
            playerHPText.text = $"{player.CurrentHP}/{player.maxHP}";

            enemyHP.DOFillAmount((float)enemy.CurrentHP / enemy.maxHP, 0.25f);
            enemyHPText.text = $"{enemy.CurrentHP}/{enemy.maxHP}";
        }

        public void SetEnemyAction(string text)
        {
            enemyActionText.text = $"Enemy - {text}";
        }

        public void EnablePlayerAction(bool enable)
        {
            attackButton.interactable = enable;
            defendButton.interactable = enable;
            healButton.interactable = enable;
        }
    }
}