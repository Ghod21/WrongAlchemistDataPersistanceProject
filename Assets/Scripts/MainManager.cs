using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public TMP_InputField userName;
    public TextMeshProUGUI highscoreMainMenuText;
    public string userNameText;
    public string leaderNameText;
    public int currentScore;
    public int HighScore;
    public bool objectsSet;
    public bool saved;
    public bool correctName;

    void Start()
    {
        LoadName();
        CheckLeader();
        saved = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !objectsSet)
        {
            GameObject highScoreObject = GameObject.Find("HighScore");
            highscoreMainMenuText = highScoreObject.GetComponent<TextMeshProUGUI>();
            GameObject enterNameObject = GameObject.Find("EnterName");
            userName = enterNameObject.GetComponent<TMP_InputField>();
            objectsSet = true;
        }

        if (userName != null)
        {
            userNameText = userName.text;
        }
        if (userNameText.Length >= 3 && userNameText.Length <= 10)
        {
            correctName = true;
        }

        currentScore = Counter.Instance.Count;

        if (Counter.Instance.healthLeft == 0 && !saved)
        {
            CheckLeader();
            SaveName();
            saved = true;
        }


        if (highscoreMainMenuText != null)
        {
            highscoreMainMenuText.text = "Highscore: " + leaderNameText + " " + HighScore;
        }

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadName();
    }

    public void CheckLeader()
    {
        if (currentScore > HighScore)
        {
            HighScore = currentScore;
            leaderNameText = userNameText;
        }
    }

    string CompareValues(string str1, int value1, string str2, int value2)
    {
        if (value1 > value2)
        {
            return str1;
        }
        else if (value2 > value1)
        {
            return str2;
        }
        else
        {
            return str2;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string leaderNameText;
        public int HighScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.leaderNameText = leaderNameText;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Data saved: " + json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            leaderNameText = data.leaderNameText;
            HighScore = data.HighScore;
            Debug.Log("Data loaded: " + json);
        }
        else
        {
            Debug.LogWarning("Save file not found at " + path);
        }
    }
}
