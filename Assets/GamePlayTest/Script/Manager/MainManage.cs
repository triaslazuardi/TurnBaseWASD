using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Manager
{
    public class MainManage : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private bool enableDebugLog = false;

        private void Awake()
        {
            Debug.unityLogger.logEnabled = enableDebugLog;
        }

        public void OnPlayGame()
        {
            SceneTransition.Instance.FadeOut("GamePlay");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}