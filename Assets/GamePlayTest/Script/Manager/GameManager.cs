using Nitss.Utility;
using Nitzz.Dialogue;
using Nitzz.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool HasKey;
        public bool canMove = true;

        [Header("Player")]
        public Transform parentPlayer;
        [SerializeField] private PlayerController playerPrefab;
        public PlayerController playerController;

        [Space,Header("Camera")]
        [SerializeField] private CameraFollow cameraPlayer;

        public DialogueSO dialogIntro;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            playerController = Instantiate(
                playerPrefab,
                parentPlayer.position,
                Quaternion.identity,
                parentPlayer);

            cameraPlayer.SetupTarget(playerController.gameObject.transform);

            DialogueManager.Instance.StartDialogue(dialogIntro);
        }
    }
}