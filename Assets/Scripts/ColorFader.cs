using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorFader : MonoBehaviour
{
    public Color startColor;
    public Color targetColor;
    public float fadeDuration = 1f;

    private Image imageComponent;
    private bool isFadingForward = true; 

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.color = startColor;
        }

        StartCoroutine(FadeColor());
    }

    private IEnumerator FadeColor()
    {
        while (true)
        {
            float time = 0f;
            Color initialColor = isFadingForward ? startColor : targetColor;
            Color target = isFadingForward ? targetColor : startColor;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / fadeDuration);

                Color currentColor = Color.Lerp(initialColor, target, t);

                if (imageComponent != null)
                {
                    imageComponent.color = currentColor;
                }

                yield return null;
            }
            isFadingForward = !isFadingForward;
        }
    }
}
