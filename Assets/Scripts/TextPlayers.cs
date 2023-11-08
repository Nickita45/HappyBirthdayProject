using System.Collections;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEditor.PackageManager;
using System.Collections.Generic;

public class TextPlayers : MonoBehaviour
{
    public TextTypingEffect textDisplay;

    private Texts textsData;
    private int currentTextIndex = 0;

    private void Start()
    {
        // Загрузка текстов из файла JSON
        string filePath = "Assets/Texts/Scene1.json"; // Укажите путь к вашему файлу JSON
        
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
            textDisplay.fullText = textsData.textArray[currentTextIndex].text;
            
            Sprite spriteAsset = Resources.Load<Sprite>("Images/"+textsData.textArray[currentTextIndex].src);
            textDisplay.imageBackground.sprite = spriteAsset;
            textDisplay.playDisplayText();
            //textDisplay.text = texts[currentTextIndex];
            currentTextIndex++;
        }
        else
        {
            textDisplay.fullText = "Все тексты воспроизведены.";
        }
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
}