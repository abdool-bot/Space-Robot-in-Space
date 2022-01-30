using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeTaker : MonoBehaviour
{
    private float mapTime = 0;
    private bool timerStart = false;
    private string highscoreString;
    private int minutes;
    
    public GameObject finishGame;
    public Text timeDisplay;
    

    private void Start()
    {
        minutes = 0;
        highscoreString = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.GetFloat(highscoreString) <= 1f)
        {
            PlayerPrefs.SetFloat(highscoreString, 999999f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (this.tag)
        {
            case "Start":
                RestartTimer();
                Debug.Log(PlayerPrefs.GetFloat(highscoreString));
                Debug.Log(highscoreString);
                break;
            case "Finish":
                StopTimer();
                finishGame.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
    }

    private void Update()
    {
        if (timerStart)
        {
            // mapTime is in seconds
            // deltaTime simulates 100 frames per second
            // meaning mapTime is increment 100 times per second by 0.01
            mapTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(mapTime / 60);
                timeDisplay.text = "Time: " + (minutes).ToString("F0") + ":" + (mapTime%60<10?"0":"") +(mapTime%60).ToString("F2");
        }
    }

    private void RestartTimer()
    {
        mapTime = 0;
        timerStart = true;
    }

    private void StopTimer()
    {
        timerStart = false;
        // ToString("F1") cuts anything after the 1st decimal value off
        if (mapTime < PlayerPrefs.GetFloat(highscoreString))
        {
            PlayerPrefs.SetFloat(highscoreString, mapTime);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat(highscoreString, 999999f);
    }

    public float GetMapTime()
    {
        return mapTime;
    }

    public float GetHighscore()
    {
        return PlayerPrefs.GetFloat(highscoreString);
    }
}