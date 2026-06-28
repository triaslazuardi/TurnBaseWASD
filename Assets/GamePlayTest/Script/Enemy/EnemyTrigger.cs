using Nitzz.Dialogue;
using Nitzz.Manager;
using Nitzz.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        [SerializeField] private Transform playerStandPoint;

        public DialogueSO dialogNoKey;
        public DialogueSO dialoEnemy;

        [SerializeField]private bool triggered;
        [SerializeField] private Collider2D callCollider;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (triggered)
                return;

            if (!other.CompareTag("Player"))
                return;

            if (!GameManager.Instance.HasKey)
            {
                DialogueManager.Instance.StartDialogue(dialogNoKey);
                return;
            }

            triggered = true;
            callCollider.enabled = false;

            GameManager.Instance.playerController.AutoMove(playerStandPoint.position, () =>
            {
                DialogueManager.Instance.StartDialogue(dialoEnemy, GoToBattle);
            });
        }

        private void GoToBattle() {
            SceneTransition.Instance.FadeOut("GameBattle");
        }

    }
    
}