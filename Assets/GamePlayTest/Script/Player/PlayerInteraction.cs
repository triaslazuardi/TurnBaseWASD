using Nitzz.Dialogue;
using Nitzz.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nitzz.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private NPCController currentNpc;

        private void Update()
        {
            if (DialogueManager.Instance.IsDialoguePlaying)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentNpc?.Interact();
            }
        }

        private void Start()
        {
            enabled = SceneManager.GetActiveScene().name == "GamePlay";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            NPCController npc = other.GetComponent<NPCController>();

            if (npc != null)
            {
                currentNpc = npc;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            NPCController npc = other.GetComponent<NPCController>();

            if (npc == currentNpc)
            {
                currentNpc = null;
            }
        }
    }
}