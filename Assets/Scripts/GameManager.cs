
using System;
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
        Debug.Log("ApplyChoice");

        

        
        if (valueB <= 1)
        {
            
            LoadScene("Scene 17.1"); // venner under 1
        }
        else if (valueA <= 1)
        {
            
            LoadScene("Scene 20.1"); //studievejleder
        }
        else
        {
            // tilføjer choicedatas scene navn til listen
          
            LoadScene(choice.nextScene);
            
        }


    }

    public void MovieEnded()
    {
        Debug.Log(string.Join(", ", sceneHistory));

        SaveHistoryToFile();
        if (sceneHistory.Contains("Scene 16.1"))
        {
            //Load credit til 16.1

            LoadScene("Credits 16.1");
        }
        else if (valueC <= 1)
        {

            LoadScene("Scene 16.1"); // penge
        }
        else if (sceneHistory.Contains("Scene 14-15") && sceneHistory.Contains("Scene 16") && !sceneHistory.Contains("Scene 18")) // har taget et job
        {

            LoadScene("Scene 18");
        }
        else if (sceneHistory.Contains("Scene 13-15.1") && sceneHistory.Contains("Scene 16") && !sceneHistory.Contains("Scene 19-21")) // har taget et job
        {

            LoadScene("Scene 19-21");
        }
        else if (valueC > 1 && !sceneHistory.Contains("Scene 17-18") && !sceneHistory.Contains("Scene 16"))
        {
            LoadScene("Scene 16");
            ////tømmer listen
            //sceneHistory.Clear();
        }
        else if (sceneHistory.Contains("Scene 17.1"))
        {
            //Load credit til 17.1

            LoadScene("Credits 17.1");
            ////tømmer listen
            //sceneHistory.Clear();
        }
        else if (sceneHistory.Contains("Scene 20.1"))
        {
            //Load credit til 20.1

            LoadScene("Credits 20.1");
            ////tømmer listen
            //sceneHistory.Clear();
        }
        else if (valueA < 3 && !sceneHistory.Contains("Karakter 4") && sceneHistory.Contains("Scene 19-21") || sceneHistory.Contains("Scene 20-21"))
        {
            //load credits til 21
            LoadScene("Karakter 4");
        }
        else if (valueA < 5 && !sceneHistory.Contains("Karakter 7") && sceneHistory.Contains("Scene 19-21") || sceneHistory.Contains("Scene 20-21"))
        {
            //load credits til 21
            LoadScene("Karakter 7");
        }
        else if (valueA < 10 && !sceneHistory.Contains("Karakter 10") && sceneHistory.Contains("Scene 19-21") || sceneHistory.Contains("Scene 20-21"))
        {
            //load credits til 21
            LoadScene("Karakter 10");

        }
        else
        {
            LoadScene("Credits hue");
            ////tømmer listen
            //sceneHistory.Clear();
        }


        

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
       

        // Nulstil værdier
        valueA = start_valueA;
        valueB = start_valueB;
        valueC = start_valueC;

        // playCount stiger og tilføj til den nye liste
        playCount++;
        sceneHistory.Add(playCount.ToString());

       

        LoadScene("Scene 1");
    }
    public void LoadScene(string sceneName)
    {
        sceneHistory.Add(sceneName);
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void OnVideoFinished(bool choiceUIWasShown)
    {
        Debug.Log("OnVideoFinished()");

        if (hasChosen == false && choiceUIWasShown == false) // film slut
        {
            Debug.Log("!hasChosen && choiceUIWasShown == false");
            //LoadScene(creditsSceneName);
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