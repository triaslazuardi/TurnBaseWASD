using DG.Tweening;
using Nitzz.Dialogue;
using Nitzz.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.NPC
{
    public class NPCKeyController : NPCController
    {
        public DialogueSO dialogueBringKey;
        private bool alreadyGiven;

        [Header("Key")]
        [SerializeField] private Transform keyIcon;

        public override void Interact()
        {
            if (alreadyGiven)
            {
                base.Interact();
                return;
            }

            DialogueManager.Instance.StartDialogue(
                dialogue,
                GiveKey);
        }

        private void GiveKey()
        {
            alreadyGiven = true;
            GameManager.Instance.HasKey = true;
            PlayKeyAnimation();
        }

        private void PlayKeyAnimation()
        {
            GameManager.Instance.canMove = false;
            keyIcon.gameObject.SetActive(true);

            keyIcon.localScale = Vector3.zero;
            keyIcon.localPosition = new Vector3(0, 1.2f, 0);

            Sequence seq = DOTween.Sequence();

            seq.Append(keyIcon.DOScale(1f, 0.25f).SetEase(Ease.OutBack));

            seq.Join(
                keyIcon.DOLocalMoveY(1.8f, 1f)
            );

            seq.AppendInterval(0.3f);

            seq.Append(
                keyIcon.DOScale(0f, 0.2f)
            );

            seq.OnComplete(() =>
            {
                keyIcon.gameObject.SetActive(false);
                DialogueManager.Instance.StartDialogue(dialogueBringKey);
            });
        }
    }
}