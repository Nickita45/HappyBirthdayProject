using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlideshow : MonoBehaviour
{
    public Image image;
    public Sprite[] images;
    public float fadeDuration = 1.0f;
    public float displayDuration = 2.0f;

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(StartSlideshow());
    }

    IEnumerator StartSlideshow()
    {
        while (true)
        {
            yield return StartCoroutine(FadeImage(true));

            yield return new WaitForSeconds(displayDuration);

            yield return StartCoroutine(FadeImage(false));

            // Переход к следующей картинке в массиве.
            image.sprite = images[currentIndex];
            currentIndex = (currentIndex + 1) % images.Length;
        }
    }

    IEnumerator FadeImage(bool fadeIn)
    {
        float targetAlpha = fadeIn ? 1.0f : 0.0f;
        float currentAlpha = image.color.a;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            image.color = newColor;
            yield return null;
        }

        // Убеждаемся, что альфа окончательно установлена в целевое значение.
        Color finalColor = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
        image.color = finalColor;
    }
}
