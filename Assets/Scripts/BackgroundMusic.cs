using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public Counter counter;
    // Start is called before the first frame update
    void Start()
    {
        GameObject counterObject = GameObject.Find("Counter");
        counter = counterObject.GetComponent<Counter>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.healthLeft > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
