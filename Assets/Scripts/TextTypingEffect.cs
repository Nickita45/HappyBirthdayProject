using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingEffect : MonoBehaviour
{
    public float letterDelay = 0.1f; // Задержка между буквами
    public string fullText = "Ваш текст для постепенного появления";
    public AudioSource audioSource;
    public AudioSource audioSourceSound;
    public TextMeshProUGUI textMeshPro;
    private string currentText = "";
    private int currentIndex = 0;
    private bool isTextFullyDisplayed = false;
    public Button buttonNext;
    public TextMeshProUGUI nameCharacter;
    public Image characterImage;
    public Image imageBackground;
    public Button[] buttonAnswers;
    public GameObject answersPanel;
    private void Start()
    {
        // textMeshPro = GetComponent<TextMeshProUGUI>();
        // playDisplayText();
    }
    public void playDisplayText()
    {
        answersPanel.SetActive(false);
        currentIndex = 0;
        currentText = "";
        StartCoroutine(DisplayText());
        characterImage.gameObject.SetActive(true);
        buttonNext.gameObject.SetActive(false);
        
        if(characterImage.sprite == null)
            characterImage.gameObject.SetActive(false);
    }
    public void playAnswersText(string[] answers)
    {
        answersPanel.SetActive(true);
        for(int i=0;i<buttonAnswers.Length;i++)
        {
            buttonAnswers[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
        }
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
