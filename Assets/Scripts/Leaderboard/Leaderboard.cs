using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        [Header("3 Text Fields containing the ranking, name and time")]
        [SerializeField] private TMP_Text[] textFields;
        [Header("")]
        [SerializeField] private int levelNumber;

        private string fileName;
        private float[] times;
        private LeaderboardData lbData;

        // Start is called before the first frame update
        private void Awake()
        {
            fileName = "leaderboard" + levelNumber + ".sav";
            lbData = new LeaderboardData();
            if (FileManager.FileExists(fileName))
            {
                lbData.FromJSON(FileManager.LoadFromFile(fileName));
            }
        }

        public void UpdateLeaderboardText()
        {
            foreach (var textField in textFields)
            {
                textField.text = "";
            }
            if (lbData.lbentries != null)
            {
                SortAndLimitLeaderboard(lbData);
                var i = 1;
                foreach (var lbE in lbData.lbentries)
                {
                    textFields[0].text +=  ""+i+".\n";
                    textFields[1].text += lbE.name.ToLower().Substring(0,Mathf.Clamp(15,0,lbE.name.Length)) + "\n";
                    textFields[2].text += TimeTaker.FormatTime(lbE.time) + "\n";
                    i++;
                }
            }
        }
    
        public void SaveEntry(string playerName, float playerTime)
        {
            lbData.lbentries ??= new List<LeaderboardData.LBentry>();
            lbData.lbentries.Add(new LeaderboardData.LBentry()
            {
                name = playerName,
                time = playerTime
            });
            
            SortAndLimitLeaderboard(lbData);

            var json = lbData.ToJSON();

            FileManager.WriteToFile(fileName, json);
        }

        // Sorts the leaderboard by fastest time and deletes all entries after the tenth.
        private void SortAndLimitLeaderboard(LeaderboardData ld)
        {
            if (ld.lbentries.Count < 2) return;
            ld.lbentries.Sort((entry1, entry2) =>
            {
                var compare = entry1.time >= entry2.time ? 1 : -1;
                return compare;
            });

            if (ld.lbentries.Count > 10)
            {
                for (int i = 10; i < ld.lbentries.Count; i++)
                {
                    ld.lbentries.Remove(ld.lbentries[i]);
                }
            }
        }

        public float GetLevelHighScore()
        {
            if (lbData.lbentries == null) return 0f;
            
            if (lbData.lbentries.Count == 0) return 0f;
            
            SortAndLimitLeaderboard(lbData);

            return lbData.lbentries[0].time;
        }

        public void SetLevelNumber(int lvl)
        {
            this.levelNumber = lvl;
            
            fileName = "leaderboard" + this.levelNumber + ".sav";
            lbData = new LeaderboardData();
            if (FileManager.FileExists(fileName))
            {
                lbData.FromJSON(FileManager.LoadFromFile(fileName));
            }
        }
    }
}