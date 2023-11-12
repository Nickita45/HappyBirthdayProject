
using UnityEngine;
using System.IO;

public class TextPlayers : MonoBehaviour
{
    public TextTypingEffect textDisplay;

    private Texts textsData;
    private int currentTextIndex = 0;

    private void Start()
    {

    }
    public void startReadingText(string nameScene)
    {
        // Загрузка текстов из файла JSON
        textDisplay.audioSourceSound.Stop();
        currentTextIndex = 0;
        //string filePath = $"Assets/Texts/{nameScene}.json"; // Укажите путь к вашему файлу JSON
        string filePath = Path.Combine(Application.streamingAssetsPath, $"Texts/{nameScene}.json");
        if (File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            textsData = JsonUtility.FromJson<Texts>(jsonText);
        }
        else
        {
            // textsData = new Texts { textArray = new string[] { "Файл с текстами не найден." } };
        }
       

        PlayNextText();
    }

    public void PlayNextText()
    {
        if (currentTextIndex < textsData.textArray.Length)
        {
            string[] answers = TryToSplitAnswers(textsData.textArray[currentTextIndex].text);
            if(answers.Length>1)
            {
                textDisplay.playAnswersText(answers);
            }
            else
            {
                textDisplay.fullText = textsData.textArray[currentTextIndex].text;
                
                Sprite spriteAsset = Resources.Load<Sprite>("Images/"+textsData.textArray[currentTextIndex].src);
                textDisplay.imageBackground.sprite = spriteAsset;
                Sprite characterImageAsset = Resources.Load<Sprite>("Characters/"+textsData.textArray[currentTextIndex].characters);
                textDisplay.characterImage.sprite = characterImageAsset;
                textDisplay.nameCharacter.text = textsData.textArray[currentTextIndex].author;
                AudioClip audioClip = Resources.Load<AudioClip>("Sounds/"+textsData.textArray[currentTextIndex].srcSound);
                if(audioClip != null)
                {
                    textDisplay.audioSourceSound.clip = audioClip;
                    textDisplay.audioSourceSound.Play();
                }
                textDisplay.playDisplayText();
            }
            //textDisplay.text = texts[currentTextIndex];
            currentTextIndex++;
        }
        else
        {
            GameManager.Instance.UpdateScenarios();
            //textDisplay.fullText = "Все тексты воспроизведены.";
        }
    }
    public string[] TryToSplitAnswers(string str)
    {
        string[] arr = str.Split("|");
        return arr;
    }
}
[System.Serializable]
public class Texts
{
    public ObjectText[] textArray;
}
[System.Serializable]
public class ObjectText
{
    public string author;
    public string text;
    public string src;
    public string characters;
    public string srcSound;
}