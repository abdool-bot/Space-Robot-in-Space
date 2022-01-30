using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTimeScript : MonoBehaviour
{
    /*
     * This script controls the Win Screen Time Display
     */
    public GameObject winScreen;
    public Text winText;
    public TimeTaker timeTaker;

    private void Update()
    {
        if (winScreen.activeSelf)
        {
            winText.text = "Your Time: " + mapTimeDisplay() +"\nHighscore: " + HighscoreDisplay();
        }
    }

    private string mapTimeDisplay()
    {
        float mapTime = timeTaker.GetMapTime();
        return (Mathf.Floor(mapTime / 60)).ToString("F0") + ":" + (mapTime % 60 < 10 ? "0" : "") +
               (mapTime % 60).ToString("F2");
    }

    private string HighscoreDisplay()
    {
        float hiscore = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
        return (Mathf.Floor(hiscore / 60)).ToString("F0") + ":" + (hiscore % 60 < 10 ? "0" : "") +
               (hiscore % 60).ToString("F2");
    }
}
