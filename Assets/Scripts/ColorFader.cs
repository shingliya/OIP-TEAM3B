using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorFader : MonoBehaviour
{
    public Color startColor;
    public Color targetColor;
    public float fadeDuration = 1f;

    private Image imageComponent;
    private bool isFadingForward = true; // Flag to determine if we are currently fading forward or backward

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

                // Interpolate the color between initialColor and target using t as the time value
                Color currentColor = Color.Lerp(initialColor, target, t);

                if (imageComponent != null)
                {
                    imageComponent.color = currentColor;
                }

                yield return null;
            }

            // Reverse the fade direction
            isFadingForward = !isFadingForward;
        }
    }
}
