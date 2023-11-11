using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ImageVideoSlideshow : MonoBehaviour
{
    public RawImage rawImage;
    public VideoClip[] videos;
    public Sprite[] images;
    public float imageDisplayDuration = 2.0f;
    public float videoDisplayDuration = 4.0f;
    public float imageFreezeDuration = 1.0f;
    public float videoShakeIntensity = 0.1f;

    private int currentIndex = 0;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        StartCoroutine(StartSlideshow());
    }

    IEnumerator StartSlideshow()
    {
        while (true)
        {
            // Показываем картинку
            rawImage.texture = images[currentIndex].texture;
            yield return new WaitForSeconds(imageFreezeDuration);
            yield return StartCoroutine(TransitionImage(true));

            // Ожидаем перед показом видео
            yield return new WaitForSeconds(imageDisplayDuration);

            // Показываем видео с эффектами
            videoPlayer.clip = videos[currentIndex];
            videoPlayer.Play();
            yield return StartCoroutine(ShakeScreen());
            yield return new WaitForSeconds(videoDisplayDuration);
            videoPlayer.Stop();
            yield return StartCoroutine(TransitionImage(false));

            // Переход к следующей картинке и видео
            currentIndex = (currentIndex + 1) % Mathf.Min(images.Length, videos.Length);
        }
    }

    IEnumerator TransitionImage(bool fadeIn)
    {
        float targetAlpha = fadeIn ? 1.0f : 0.0f;
        float currentAlpha = rawImage.color.a;
        float elapsedTime = 0.0f;

        while (elapsedTime < imageFreezeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / imageFreezeDuration);
            Color newColor = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, newAlpha);
            rawImage.color = newColor;
            yield return null;
        }

        // Убеждаемся, что альфа окончательно установлена в целевое значение.
        Color finalColor = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, targetAlpha);
        rawImage.color = finalColor;
    }

    IEnumerator ShakeScreen()
    {
        Vector3 originalPosition = transform.position;

        float elapsedTime = 0.0f;
        while (elapsedTime < videoDisplayDuration)
        {
            elapsedTime += Time.deltaTime;
            float xOffset = Random.Range(-1f, 1f) * videoShakeIntensity;
            float yOffset = Random.Range(-1f, 1f) * videoShakeIntensity;

            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            yield return null;
        }

        // Возвращаем экран на место
        transform.position = originalPosition;
    }
}
