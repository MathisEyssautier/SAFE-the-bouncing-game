using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
    public AudioSource checkSound;
    public TextMeshProUGUI CounterText;
    public TextMeshProUGUI HighScoreText;

    private int Count = 0;

    private void Start()
    {
        Count = 0;
        HighScoreText.text = "HS : " + PlayerPrefs.GetInt("Highscore", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Good"))
        {
            Count += 1;
            CounterText.text = "Score : " + Count;
            Destroy(other.gameObject);
            checkSound.Play();
            CheckHighScore();
        }
        
    }

    private void CheckHighScore()
    {
        if (Count > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", Count);
        }
    }
}
