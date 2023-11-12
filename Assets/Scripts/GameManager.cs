using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentScenarios = 0;
    //private string[] nameScenarios = {"Scene1","Scene2","Scene3Borik","Scene3BorikAfter","Scene4Dmitry","Scene4DmitryAfter","Scene5MaksymIlyaAfter"};
    private string[] nameScenarios = {"Scene6"};
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public GameObject credits, maingame;

    public TextPlayers textPlayers;
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

        UpdateScenarios();
    }
    public void UpdateScenarios()
    {
        if(currentScenarios == nameScenarios.Length)
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
}