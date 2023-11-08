using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingEffect : MonoBehaviour
{
    public float letterDelay = 0.1f; // Задержка между буквами
    public string fullText = "Ваш текст для постепенного появления";
    public AudioSource audioSource;
    public TextMeshProUGUI textMeshPro;
    private string currentText = "";
    private int currentIndex = 0;
    private bool isTextFullyDisplayed = false;
    public Button buttonNext;

    public Image imageBackground;
    private void Start()
    {
        // textMeshPro = GetComponent<TextMeshProUGUI>();
        // playDisplayText();
    }
    public void playDisplayText()
    {
        currentIndex = 0;
        currentText = "";
        StartCoroutine(DisplayText());
        buttonNext.gameObject.SetActive(false);
    }
    private IEnumerator DisplayText()
    {
        audioSource.Play();
        while (currentIndex < fullText.Length)
        {
            currentText += fullText[currentIndex];
            textMeshPro.text = currentText;
            currentIndex++;
            yield return new WaitForSeconds(letterDelay);
        }
        buttonNext.gameObject.SetActive(true);
        audioSource.Stop();
        isTextFullyDisplayed = true;
    }
    
    // Вызывайте этот метод, чтобы сразу отобразить весь текст
    public void DisplayFullText()
    {
        if (!isTextFullyDisplayed)
        {
            StopAllCoroutines();
            textMeshPro.text = fullText;
            isTextFullyDisplayed = true;
            audioSource.Stop();
        }
    }
}
