using System.Collections;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEditor.PackageManager;
using System.Collections.Generic;
using System;

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
                textDisplay.playDisplayText();
            }
            //textDisplay.text = texts[currentTextIndex];
            currentTextIndex++;
        }
        else
        {
            textDisplay.fullText = "Все тексты воспроизведены.";
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
}