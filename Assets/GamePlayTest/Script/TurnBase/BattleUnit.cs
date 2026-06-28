using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nitzz.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [Header("Stats")]
        public int maxHP = 100;
        public int attack = 20;
        public int healAmount = 20;

        public int CurrentHP;

        public bool IsDefending;

        private Vector3 startPosition;

        [Header("Efect")]
        [SerializeField] private Transform popupPoint;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            CurrentHP = maxHP;
            startPosition = transform.position;
        }

        private void Start()
        {
            enabled = SceneManager.GetActiveScene().name == "GameBattle";
        }

        public void ResetDefend()
        {
            IsDefending = false;
        }

        public void Defend()
        {
            IsDefending = true;

            transform.DOPunchRotation(
            new Vector3(0, 0, 8),
            .25f);
        }

        public void Heal()
        {
            CurrentHP += healAmount;

            if (CurrentHP > maxHP)
                CurrentHP = maxHP;

            transform.DOPunchScale(
            Vector3.one * .15f,
            .3f);

            DamageText popup = Instantiate(BattleManager.Instance.damagePopupPrefab,
            popupPoint.position,
            Quaternion.identity, BattleManager.Instance.parentFlowtext);

            popup.Show(healAmount, EnemyAction.Heal);
        }

        public void TakeDamage(int damage)
        {
            if (IsDefending)
                damage /= 2;

            CurrentHP -= damage;

            if (CurrentHP < 0)
                CurrentHP = 0;

            IsDefending = false;

            DamageText popup = Instantiate(BattleManager.Instance.damagePopupPrefab,
            popupPoint.position,
            Quaternion.identity, BattleManager.Instance.parentFlowtext);

            FlashDamage();
            popup.Show(damage);
        }

        public void PlayAttack(Vector3 target, System.Action callback)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(target, .18f));

            sequence.Append(transform.DOPunchScale(
                Vector3.one * .12f,
                .12f));

            sequence.Append(transform.DOMove(startPosition, .18f));

            sequence.OnComplete(() =>
            {
                callback?.Invoke();
            });
        }

        private void FlashDamage()
        {
            spriteRenderer.DOColor(Color.red, .08f)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}