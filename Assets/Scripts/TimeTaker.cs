using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeTaker : MonoBehaviour
{
    private float mapTime = 0;
    private bool timerStart = false;
    private Leaderboard.Leaderboard leaderboard;
    private string playerName = "placeholder";
    
    [SerializeField] private GameObject finishGame;
    [SerializeField] private TMP_Text timeDisplay, finishText;
    [SerializeField] private PlayerName playerNameAsset;

    public float HighScore { get; private set; }


    private void Start()
    {
        Time.timeScale = 1f;
        if(!string.IsNullOrEmpty(playerNameAsset.Name)) playerName = playerNameAsset.Name;
        leaderboard = this.GetComponent<Leaderboard.Leaderboard>();
        mapTime = 0;
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
            leaderboard.SaveEntry(playerName,mapTime);
            HighScore = leaderboard.GetLevelHighScore();
            UpdateFinishText();
        }
        
        timerStart = false;
    }

    #endregion

    #region FinishDisplay

    private void UpdateFinishText()
    {
        finishText.text = "your time: " + FormatTime(mapTime) +"\nhigh score: " + FormatTime(HighScore);
    }

    #endregion

    public static string FormatTime(float time)
    {
        var minutes=Mathf.Floor(time/60);
        var seconds = Mathf.Floor(time % 60);
        var milliseconds = (time * 1000) % 1000;
        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }
}