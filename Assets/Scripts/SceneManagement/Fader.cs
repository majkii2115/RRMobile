using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour 
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;
        private void Start() 
        {
            canvasGroup = FindObjectOfType<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(1f);
            yield return FadeIn(0.5f);
        }

        public IEnumerator FadeOut(float time)
        {
            return Fade(time, 1);
        }       
        public IEnumerator FadeIn(float time)
        {
            return Fade(time, 0);
        }
        public IEnumerator Fade(float time, float target)
        {
            if(currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(time, target));
            yield return currentActiveFade;
        }
        private IEnumerator FadeRoutine(float time, float target) 
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}