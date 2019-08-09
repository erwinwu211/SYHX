using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Fade : SingletonMonoBehaviour<Fade>
{
    public event Action OnFadeComplete;
    public Image fade;
    public Color startCol;
    public float fadeInDuration;
    public float fadeOutDuration;
    private Color endCol = Color.black;
    public bool m_IsFading;
    protected override void UnityAwake()
    {
    }

    public IEnumerator FadeIn()
    {
        m_IsFading = true;
        this.gameObject.SetActive(true);
        // Execute this loop once per frame until the timer exceeds the duration.
        float timer = 0f;
        while (timer <= fadeInDuration)
        {
            // Set the colour based on the normalised time.
            fade.color = Color.Lerp(startCol, endCol, timer / fadeInDuration);

            // Increment the timer by the time between frames and return next frame.
            timer += Time.deltaTime;
            yield return null;
        }
        // Fading is finished so allow other fading calls again.
        m_IsFading = false;
        this.gameObject.SetActive(false);
        // If anything is subscribed to OnFadeComplete call it.
        if (OnFadeComplete != null)
            OnFadeComplete();
    }

    public IEnumerator FadeOut()
    {
        m_IsFading = true;
        this.gameObject.SetActive(true);
        // Execute this loop once per frame until the timer exceeds the duration.
        float timer = 0f;
        while (timer <= fadeInDuration)
        {
            // Set the colour based on the normalised time.
            fade.color = Color.Lerp(endCol, startCol, timer / fadeInDuration);

            // Increment the timer by the time between frames and return next frame.
            timer += Time.deltaTime;
            yield return null;
        }
        // Fading is finished so allow other fading calls again.
        m_IsFading = false;
        this.gameObject.SetActive(false);
        // If anything is subscribed to OnFadeComplete call it.
        if (OnFadeComplete != null)
            OnFadeComplete();
    }

    public IEnumerator FadePingPong()
    {
        // Fading is now happening.  This ensures it won't be interupted by non-coroutine calls.
        m_IsFading = true;
        this.gameObject.SetActive(true);
        // Execute this loop once per frame until the timer exceeds the duration.
        float timer = 0f;
        while (timer <= fadeInDuration)
        {
            // Set the colour based on the normalised time.
            fade.color = Color.Lerp(startCol, endCol, timer / fadeInDuration);

            // Increment the timer by the time between frames and return next frame.
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        while (timer <= fadeOutDuration)
        {
            // Set the colour based on the normalised time.
            fade.color = Color.Lerp(endCol, startCol, timer / fadeOutDuration);
            // Increment the timer by the time between frames and return next frame.
            timer += Time.deltaTime;
            yield return null;
        }

        // Fading is finished so allow other fading calls again.
        m_IsFading = false;
        this.gameObject.SetActive(false);
        // If anything is subscribed to OnFadeComplete call it.
        if (OnFadeComplete != null)
            OnFadeComplete();
    }
}
