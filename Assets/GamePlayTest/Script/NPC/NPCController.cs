using Nitzz.Dialogue;
using Nitzz.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.NPC
{
    public class NPCController : MonoBehaviour
    {
        public DialogueSO dialogue;
        public DialogueSO dialogueAfterKey;

        public virtual void Interact()
        {
            if (GameManager.Instance.HasKey) {
                DialogueManager.Instance.StartDialogue(dialogueAfterKey);
                return;
            }

            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }
}