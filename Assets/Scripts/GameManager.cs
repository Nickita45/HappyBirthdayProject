using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentScenarios = 0;
    private string[] nameScenarios = {"Scene5MaksymIlyaAfter","Scene4Dmitry","Scene4DmitryAfter"   ,"Scene1","Scene2","Scene3Borik","Scene3BorikAfter","Scene4Dmitry","Scene4DmitryAfter","Scene5MaksymIlyaAfter"};
    private static GameManager _instance;
    public static GameManager Instance => _instance;

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
        textPlayers.startReadingText(nameScenarios[currentScenarios]);
        currentScenarios++;
    }
}