using TMPro;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerName", menuName = "Variables/New Player Name")]
    public class PlayerName : ScriptableObject
    {
        public string Name { get; private set; }

        public void SetName(TMP_Text nameText)
        {
            if (string.IsNullOrEmpty(nameText.text)) Name = "placeholder";
            if (nameText.text != null) Name = nameText.text.ToLower();
        }
    }
}
