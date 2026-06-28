using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nitzz.Battle
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text damageText;

        [SerializeField] private Color[] allColorText;

        public void Show(int damage, EnemyAction act = EnemyAction.Attack)
        {
            Color actColorText = allColorText[(int)act];
            damageText.text = damage.ToString();
            damageText.color = actColorText;    

            transform.DOMoveY(transform.position.y + 3f, 1f);

            damageText.DOFade(0, 1f)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}