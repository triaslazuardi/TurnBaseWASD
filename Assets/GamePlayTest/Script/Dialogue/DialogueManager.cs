using Nitzz.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Nitzz.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [Header("UI")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TMP_Text speakerText;
        [SerializeField] private TMP_Text dialogueText;

        private DialogueSO currentDialogue;
        private int currentIndex;
        private UnityAction onComplete;

        public bool IsDialoguePlaying;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            if (!IsDialoguePlaying)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }

        public void StartDialogue(DialogueSO dialogue, UnityAction callback = null)
        {
            GameManager.Instance.canMove = false;
            currentDialogue = dialogue;
            currentIndex = 0;
            onComplete = callback;

            IsDialoguePlaying = true;
            dialoguePanel.SetActive(true);

            ShowLine();
        }

        private void ShowLine()
        {
            Debug.Log($"ShowLine kok idx: {currentIndex}");
            DialogueLine line = currentDialogue.lines[currentIndex];
            Debug.Log($"ShowLine kok idx: {line.text}");
            speakerText.text = line.speakerName;
            dialogueText.text = line.text;
        }

        private void NextLine()
        {
            Debug.Log("space kok");
            currentIndex++;

            if (currentIndex >= currentDialogue.lines.Length)
            {
                Debug.Log("space kok 2");
                EndDialogue();
                return;
            }

            ShowLine();
        }

        private void EndDialogue()
        {
            GameManager.Instance.canMove = true;
            StartCoroutine(EnableDialogueAgain());

            dialoguePanel.SetActive(false);

            onComplete?.Invoke();
        }

        private IEnumerator EnableDialogueAgain()
        {
            yield return new WaitForSeconds(0.2f);
            IsDialoguePlaying = false;
        }
    }
}