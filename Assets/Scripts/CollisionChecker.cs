using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private Counter counter;

    public string redJug = "RedJug";
    public string redPot = "RedCollider";
    public string greenJug = "GreenJug";
    public string greenPot = "GreenCollider";
    public string blueJug = "BlueJug";
    public string bluePot = "BlueCollider";

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private float soundVolumeOne = 0.3f;

    private int streakMultiplier = 1;

    public int streakLevel;
    [SerializeField] int streak;

    private void Start()
    {
        GameObject counterObject = GameObject.Find("Counter");
        counter = counterObject.GetComponent<Counter>();
        streak = 25;
        streakLevel = 3;
    }

    private void Update()
    {
        if (streak >= 0 && streak <= 9)
        {
            streakLevel = 0;
            streakMultiplier = 1;
        }
        if (streak >= 10 && streak <= 24)
        {
            streakLevel = 1;
            streakMultiplier = 2;
        }
        if (streak >= 25 && streak <= 49)
        {
            streakLevel = 2;
            streakMultiplier = 4;
        }
        if (streak >= 50 && streak <= 99)
        {
            streakLevel = 3;
            streakMultiplier = 8;
        }
        if (streak >= 100)
        {
            streakLevel = 4;
            streakMultiplier = 16;
        }
        streak = counter.gameStreak;
        counter.gameStreakLevel = streakLevel;

        if (counter.objDestroy == true)
        {
            StreakLoss();
            counter.objDestroy = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (gameObject.CompareTag(redPot) && otherObject.CompareTag(redJug))
        {
            Debug.Log("Good Red");
            counter.gameStreak++;
            counter.Count = counter.Count + 10 * streakMultiplier;
            if (streak == 9 || streak == 24 || streak == 49 || streak == 99)
            {
                audioSource.PlayOneShot(audioClips[3], soundVolumeOne);
            } else
            {
                audioSource.PlayOneShot(audioClips[0], soundVolumeOne);
            }
            Destroy(otherObject);
        }
        else if (gameObject.CompareTag(redPot) && !otherObject.CompareTag(redJug))
        {
            Debug.Log("Wrong Red");
            counter.Count = (int)(counter.Count * 0.8);
            StreakLoss();
            if (counter.healthLeft > 0)
            {
                counter.healthLeft--;
            }
            if (counter.healthLeft > 0)
            {
                audioSource.PlayOneShot(audioClips[1], soundVolumeOne);
            }
            Destroy(otherObject);
        }

        if (gameObject.CompareTag(greenPot) && otherObject.CompareTag(greenJug))
        {
            Debug.Log("Good Green");
            counter.gameStreak++;
            counter.Count = counter.Count + 10 * streakMultiplier;
            if (streak == 9 || streak == 24 || streak == 49 || streak == 99)
            {
                audioSource.PlayOneShot(audioClips[3], soundVolumeOne);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[0], soundVolumeOne);
            }
            Destroy(otherObject);
        }
        else if (gameObject.CompareTag(greenPot) && !otherObject.CompareTag(greenJug))
        {
            Debug.Log("Wrong Green");
            counter.Count = (int)(counter.Count * 0.8);
            StreakLoss();
            if (counter.healthLeft > 0)
            {
                counter.healthLeft--;
            }
            if (counter.healthLeft > 0)
            {
                audioSource.PlayOneShot(audioClips[1], soundVolumeOne);
            }
            Destroy(otherObject);
        }
        if (gameObject.CompareTag(bluePot) && otherObject.CompareTag(blueJug))
        {
            Debug.Log("Good Blue");
            counter.gameStreak++;
            counter.Count = counter.Count + 10 * streakMultiplier;
            if (streak == 9 || streak == 24 || streak == 49 || streak == 99)
            {
                audioSource.PlayOneShot(audioClips[3], soundVolumeOne);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[0], soundVolumeOne);
            }
            Destroy(otherObject);
        }
        else if (gameObject.CompareTag(bluePot) && !otherObject.CompareTag(blueJug))
        {
            Debug.Log("Wrong Blue");
            counter.Count = (int)(counter.Count * 0.8);
            StreakLoss();
            if (counter.healthLeft > 0)
            {
                counter.healthLeft--;
            }
            if (counter.healthLeft > 0)
            {
                audioSource.PlayOneShot(audioClips[1], soundVolumeOne);
            }
            Destroy(otherObject);
        }
    }

    private void StreakLoss()
    {
        if (streakLevel == 4)
        {
            counter.gameStreak = 50;
            streakLevel = 3;
        }
        else if (streakLevel == 3)
        {
            counter.gameStreak = 25;
            streakLevel = 2;
        }
        else if (streakLevel == 2)
        {
            counter.gameStreak = 10;
            streakLevel = 1;
        }
        else if (streakLevel == 1)
        {
            counter.gameStreak = 0;
            streakLevel = 0;
        }
        else if (streakLevel == 0)
        {
            counter.gameStreak = 0;
            streakLevel = 0;
        }
    }


}