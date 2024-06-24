using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI streak;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI yourScoreEnd;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI logo;
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
        Count = 0;
        healthLeft = 3;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
        // Проверяем, есть ли касание на экране
        if (Input.touchCount > 0 && healthLeft == 0)
        {
            // Получаем первое касание
            Touch touch = Input.GetTouch(0);

            // Проверяем фазу касания (начало касания)
            if (touch.phase == TouchPhase.Began)
            {
                // Вызываем событие A
                Restart();
            }
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
            if (counterText != null)
            {
                counterText.text = "Score : " + Count;
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
            yourScoreEnd.text = "Your Score: " + lastScore;
        }

        if (!lossSoundPlayed && healthLeft == 0)
        {
            audioSource.PlayOneShot(audioClips[1], 0.5f);
            lossSoundPlayed = true;
        }
    }

    private void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void StartLevel()
    {
        levelStarted = true;
        audioSource.PlayOneShot(audioClips[0], 0.4f);
    }
}

