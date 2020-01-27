﻿using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            
        }
        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha != 1f)
            {
                canvasGroup.alpha += (Time.deltaTime / time);
                yield return null;
            }
            
        }
        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha >0 )
            {
                canvasGroup.alpha -= (Time.deltaTime / time);
                yield return null;
            }

        }

        public void SetAlpha(float a)
        {
            canvasGroup.alpha = a;
        }
    }

}