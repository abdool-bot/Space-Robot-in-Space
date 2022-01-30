using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{
    [System.Serializable]
    public class LeaderboardData
    {
        public LeaderboardData()
        {
            lbentries = new List<LBentry>();
        }
    
        [System.Serializable]
        public struct LBentry
        {
            public string name;
            public float time;
        }

        public List<LBentry> lbentries;

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

        public void FromJSON(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
