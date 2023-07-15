using TMPro;
using UnityEngine;

namespace F4B1.UI.Leaderboard
{
    public class LeaderboardEntry : MonoBehaviour
    {

        [Header("Components")] 
        [SerializeField] private TextMeshProUGUI rankTextField;
        [SerializeField] private TextMeshProUGUI coinsTextField;
        [SerializeField] private TextMeshProUGUI usernameTextField;
        
        public void SetLeaderboardEntryData(int rank, long coins, string username)
        {
            rankTextField.text = $"#{rank}";
            coinsTextField.text = $"{NumberFormatter.FormatNumberWithLetters(coins)} Cookies";
            usernameTextField.text = username;
        }
        
    }
}