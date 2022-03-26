using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    #region Instance
    private static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FadeManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned FadeManager", typeof(FadeManager)).GetComponent<FadeManager>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    [Header("Fade references")]
    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private Image fadeImage;

    [Header("Start Fade")]
    [SerializeField] private bool startWithFade = true;
    [SerializeField] private float startFadeTime = 1.5f;
    [SerializeField] private Color startFadeColor = Color.black;

    private void Awake()
    {
        // Keep this alive throughout the game
        DontDestroyOnLoad(this.gameObject);

        if (startWithFade)
        {
            fadeGroup.alpha = 1;
            FadeIn(startFadeTime, startFadeColor);
        }
        else
        {
            fadeGroup.alpha = 0;
        }
    }

    public void FadeOut(float transitionTime)
    {
        // Re-use last color, no action at the end
        FadeOut(transitionTime, fadeImage.color, null);
    }
    public void FadeOut(float transitionTime, Color fadeColor)
    {
        // Apply new color, no action at the end
        FadeOut(transitionTime, fadeColor, null);
    }
    public void FadeOut(float transitionTime, Color fadeColor, Action func)
    {
        fadeImage.color = fadeColor;
        StartCoroutine(UpdateFadeOut(transitionTime,func));
    }

    public void FadeIn(float transitionTime)
    {
        // Re-use last color, no action at the end
        FadeIn(transitionTime, fadeImage.color, null);
    }
    public void FadeIn(float transitionTime, Color fadeColor)
    {
        // Apply new color, no action at the end
        FadeIn(transitionTime, fadeColor, null);
    }
    public void FadeIn(float transitionTime, Color fadeColor, Action func)
    {
        fadeImage.color = fadeColor;
        StartCoroutine(UpdateFadeIn(transitionTime, func));
    }

    private IEnumerator UpdateFadeOut(float transitionTime, Action func)
    {
        float t = 0.0f;

        for (t = 0.0f; t <= 1; t += Time.deltaTime/transitionTime)
        {
            fadeGroup.alpha = t;
            yield return null;
        }

        fadeGroup.alpha = 1;
        func?.Invoke();
    }
    private IEnumerator UpdateFadeIn(float transitionTime, Action func)
    {
        float t = 0.0f;

        for (t = 0.0f; t <= 1; t += Time.deltaTime / transitionTime)
        {
            fadeGroup.alpha = 1 - t;
            yield return null;
        }

        fadeGroup.alpha = 0;
        func?.Invoke();
    }
}
