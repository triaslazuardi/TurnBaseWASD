using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Nitzz.Manager
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance;

        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private bool isActiveFirst =true;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            fadeImage.gameObject.SetActive(isActiveFirst);
            if (isActiveFirst)
            {
                FadeIn();
            }
        }

        public void FadeIn()
        {
            Color color = fadeImage.color;
            color.a = 1f;
            fadeImage.color = color;

            fadeImage.DOFade(0f, fadeDuration).OnComplete(() => { fadeImage.gameObject.SetActive(false); });
        }

        public void FadeOut(string sceneName)
        {
            fadeImage.gameObject.SetActive(true);
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.DOFade(1f, fadeDuration)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(sceneName);
                });
        }
    }
}