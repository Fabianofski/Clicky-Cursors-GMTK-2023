using System;
using System.Linq;
using F4B1.SaveSystem;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.UI.Leaderboard
{
    public class RealtimeLeaderboard : MonoBehaviour
    {

        [Header("Atoms")] 
        [SerializeField] private Int64Variable coins;
        
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI inFrontTextField;
        [SerializeField] private TextMeshProUGUI youTextField;
        [SerializeField] private TextMeshProUGUI behindTextField;

        [Header("Update")] 
        [SerializeField] private float updateRate;
        private float updateTimer;
        private SaveData[] ranking;

        private void Update()
        {
            updateTimer -= Time.deltaTime;

            UpdateLeaderboard();
            if (updateTimer >= 0) return;

            updateTimer = updateRate;
            StartCoroutine(APIManager.FetchLeaderboard(LeaderboardCallback));
        }

        private void LeaderboardCallback(SaveData[] data)
        {
            ranking = data.Where(x => x.displayName != APIManager.username).ToArray();
        }
        
        private void UpdateLeaderboard()
        {
            if (ranking == null) return;
            
            inFrontTextField.text = "";
            youTextField.text = "";
            behindTextField.text = "";
            
            for (var i = 0; i < ranking.Length; i++)
            {
                var entry = ranking[i];
                var endOfLoop = i == ranking.Length - 1;
                if (entry.coins >= coins.Value && !endOfLoop) continue;
                
                inFrontTextField.gameObject.SetActive(i > 0);

                if (endOfLoop && coins.Value < entry.coins)
                {
                    SetText(youTextField, i + 2, "You",coins.Value);
                    SetText(inFrontTextField, i + 1, entry.displayName, entry.coins);
                }
                else
                {
                    if(i > 0) SetText(inFrontTextField, i, ranking[i - 1].displayName, ranking[i - 1].coins);
                    SetText(youTextField, i + 1, "You",coins.Value);
                    SetText(behindTextField, i + 2, entry.displayName, entry.coins);
                }
                
                break;
            }
        }

        private void SetText(TextMeshProUGUI textField, int rank, string displayName, long coins)
        {
            textField.text = $"#{rank} {displayName} - {NumberFormatter.FormatNumberWithLetters(coins)} cookies";
        }
        
    }
}