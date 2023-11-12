using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentScenarios = 0;
    private string[] nameScenarios = {"Scene1","Scene2","Scene3Borik","Scene3BorikAfter","Scene4Dmitry","Scene4DmitryAfter","Scene5MaksymIlya","Scene5MaksymIlyaAfter","Scene6","Credits"};
    //private string[] nameScenarios = {"Scene4DmitryAfter","Credits"};
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public GameObject credits, maingame, menu;

    public TextPlayers textPlayers;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    // Update is called once per frame
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        inputField.onEndEdit.AddListener(new UnityAction<string>(StartGame));
        // UpdateScenarios();
    }
    public void UpdateScenarios()
    {
        if(nameScenarios[currentScenarios] == "Credits")
        {
            //final 
            credits.SetActive(true);
            maingame.SetActive(false);
        }
        else
        {
            textPlayers.startReadingText(nameScenarios[currentScenarios]);
            currentScenarios++;
        }
    }
    public void StartGame(string scene)
    {
        Debug.Log(scene);
        menu.SetActive(false);

        maingame.SetActive(true);

        currentScenarios = FindStringIndex(nameScenarios,scene);
        UpdateScenarios();
    }
    static int FindStringIndex(string[] array, string target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == target)
            {
                return i; // Если нашли, возвращаем индекс
            }
        }
        return 0; // Если не нашли, возвращаем -1
    }
}