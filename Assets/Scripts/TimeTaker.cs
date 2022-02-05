using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeTaker : MonoBehaviour
{
    private float mapTime = 0;
    private bool timerStart = false;
    private Leaderboard.Leaderboard leaderboard;
    
    [SerializeField] private GameObject finishGame;
    [SerializeField] private TMP_Text timeDisplay, finishText;

    public float HighScore { get; private set; }


    private void Start()
    {
        
        leaderboard = this.GetComponent<Leaderboard.Leaderboard>();
        mapTime = 0;
        HighScore = leaderboard.GetLevelHighScore();
    }
    
    private void Update()
    {
        if (!timerStart) return;
        // mapTime is in seconds
        // deltaTime simulates 100 frames per second
        // meaning mapTime is increment 100 times per second by 0.01
        mapTime += Time.deltaTime;
        timeDisplay.text = FormatTime(mapTime);
    }

    #region Starting_and_Stopping

    public void StartPassed()
    {
        RestartTimer();
    }

    public void FinishPassed()
    {
        StopTimer();
        finishGame.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void RestartTimer()
    {
        mapTime = 0;
        timerStart = true;
    }

    private void StopTimer()
    {
        if (timerStart)
        {
            leaderboard.SaveEntry("Placeholder",mapTime);
            UpdateFinishText();
        }
        
        timerStart = false;
    }

    #endregion

    #region FinishDisplay

    private void UpdateFinishText()
    {
        finishText.text = "Your Time: " + FormatTime(mapTime) +"\nHigh Score: " + FormatTime(HighScore);
    }

    #endregion

    public float GetMapTime()
    {
        return mapTime;
    }
    
    public static string FormatTime(float time)
    {
        var minutes=Mathf.Floor(time/60);
        var seconds = Mathf.Floor(time % 60);
        var milliseconds = (time * 1000) % 1000;
        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }
}