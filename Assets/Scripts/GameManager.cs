
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int valueA;
    public int valueB;
    public int valueC;

    private int start_valueA;
    private int start_valueB;
    private int start_valueC;
    public bool hasChosen { get; private set; }

    public string creditsSceneName = "Credits";

    // int til antal af genafspildning første gang = 1
    public int playCount { get; private set; } = 1;
    // tilføj liste af string til scene navn. scene navn fås af Choice button som sender choicedata
    public List<string> sceneHistory { get; private set; } = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //tilføjer int'en for antallet af genafspildning til listen af strings af scene navne.  
        sceneHistory.Add(playCount.ToString());
        //gemme value a, b og c i start værdier variabler a,b og c
        start_valueA = valueA;
        start_valueB = valueB;
        start_valueC = valueC;
    }

    public void ApplyChoice(ChoiceData choice)
    {
        hasChosen = true;

        valueA += choice.valueAChange;
        valueB += choice.valueBChange;
        valueC += choice.valueCChange;


        // tilføjer choicedatas scene navn til listen
        sceneHistory.Add(choice.nextScene);

        if (valueA <= 1)
        {
            LoadScene("Scene 16.1"); // penge
        }
        else if (valueB <= 1)
        {
            LoadScene("Scene 17.1"); // venner under 1
        }
        else if (sceneHistory.Contains("Scene 14;") && sceneHistory.Contains("Scene 16;")) // har taget et job
        {
            LoadScene("Scene 18");
        }
        else
        {
            LoadScene(choice.nextScene);
        }


    }

    public void MovieEnded()
    {
        Debug.Log(string.Join(", ", sceneHistory));

        SaveHistoryToFile();
        //tømmer listen
        sceneHistory.Clear();

    }
    public void SaveHistoryToFile()
    {
        string path = Application.persistentDataPath + "/sceneHistory.csv";
        string content = string.Join(";", sceneHistory);
        File.AppendAllText(path, content + "\n");
        Debug.Log("Gemt til: " + path);
    }
    public void StartNewPlaythrough()
    {
        //MovieEnded();

        // Nulstil værdier
        valueA = start_valueA;
        valueB = start_valueB;
        valueC = start_valueC;

        // Inkrementer playCount og tilføj til den nye liste
        playCount++;
        sceneHistory.Add(playCount.ToString());

        sceneHistory.Add("Scene 1");

        LoadScene("Scene 1");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnVideoFinished(bool choiceUIWasShown)
    {
        //Debug.Log("OnVideoFinished()");

        if (hasChosen == false && choiceUIWasShown == false)
        {
            //Debug.Log("!hasChosen && choiceUIWasShown == false");
            LoadScene(creditsSceneName);
            MovieEnded();

        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetChoice;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetChoice;
    }

    void ResetChoice(Scene scene, LoadSceneMode mode)
    {
        hasChosen = false;

    }
}