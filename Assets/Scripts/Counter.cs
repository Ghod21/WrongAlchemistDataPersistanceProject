using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Counter : MonoBehaviour
{
    public static Counter Instance;

    public TextMeshProUGUI counterText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI streak;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI yourScoreEnd;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI logo;
    public TextMeshProUGUI leaderText;
    public TextMeshProUGUI name;
    public Button restartButton;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public float difficulty;
    public int gameStreakLevel;
    public int gameStreak;
    public bool objDestroy;
    public int healthLeft;
    public int Count;
    private int lastScore;
    private bool lastScoreBool;
    public bool levelStarted;
    public bool lossSoundPlayed;

    private void Start()
    {
        Instance = this;
        Count = 0;
        healthLeft = 3;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            levelStarted = true;
            MainManager.Instance.objectsSet = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }

        if (leaderText != null)
        {
            leaderText.text = "Leader: " + MainManager.Instance.leaderNameText + " " + MainManager.Instance.HighScore;
        }


        if (healthLeft > 0 && levelStarted)
        {
            healthText.gameObject.SetActive(true);
            streak.gameObject.SetActive(true);
            counterText.gameObject.SetActive(true);
            difficultyText.gameObject.SetActive(true);
            yourScoreEnd.gameObject.SetActive(false);
            restartText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            logo.gameObject.SetActive(false);
            name.text = MainManager.Instance.userNameText;
            if (counterText != null)
            {
                counterText.text = "Score " + Count;
            }
            if (gameStreakLevel == 0)
            {
                difficultyText.text = "Easy";
            }
            if (gameStreakLevel == 1)
            {
                difficultyText.text = "Medium 2x";
            }
            if (gameStreakLevel == 2)
            {
                difficultyText.text = "Hard 4x";
            }
            if (gameStreakLevel == 3)
            {
                difficultyText.text = "Hell 8x";
            }
            if (gameStreakLevel == 4)
            {
                difficultyText.text = "What? 16x";
            }
            streak.text = "Streak: " + gameStreak;
            healthText.text = "Health: " + healthLeft;
        }
        else if (healthLeft == 0)
        {
            if (!lastScoreBool)
            {
                lastScore = Count;
                lastScoreBool = true;
            }
            healthText.gameObject.SetActive(false);
            streak.gameObject.SetActive(false);
            counterText.gameObject.SetActive(false);
            difficultyText.gameObject.SetActive(false);
            yourScoreEnd.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            name.gameObject.SetActive(false);
            yourScoreEnd.text = "Your Score: " + MainManager.Instance.userNameText + " " + lastScore;
        }

        if (!lossSoundPlayed && healthLeft == 0)
        {
            audioSource.PlayOneShot(audioClips[1], 0.5f);
            lossSoundPlayed = true;
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveName();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}

