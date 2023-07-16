using System;
using F4B1.SaveSystem;
using UnityEngine;

namespace F4B1.UI.Leaderboard
{
    public class LeaderboardTable : MonoBehaviour
    {

        [SerializeField] private Transform parent;

        [Header("Prefabs")] 
        [SerializeField] private GameObject firstLeaderboardEntryPrefab;
        [SerializeField] private GameObject secondLeaderboardEntryPrefab;
        [SerializeField] private GameObject thirdLeaderboardEntryPrefab;
        [SerializeField] private GameObject leaderboardEntryPrefab;
        [SerializeField] private GameObject loadingEntryPrefab;
        
        private void OnEnable()
        {
            DeleteAllChildren();
            Instantiate(loadingEntryPrefab, parent);
            StartCoroutine(APIManager.FetchLeaderboard(LeaderboardCallback));
        }

        private void LeaderboardCallback(SaveData[] ranking)
        {
            if (ranking == null) return;
            DeleteAllChildren();
            
            for (var index = 1; index - 1 < ranking.Length; index++)
            {
                var entry = ranking[index - 1];
                switch (index)
                {
                    case 1:
                        CreateLeaderboardEntry(firstLeaderboardEntryPrefab, index, entry.coins, entry.displayName);
                        break;
                    case 2:
                        CreateLeaderboardEntry(secondLeaderboardEntryPrefab, index, entry.coins, entry.displayName);
                        break;
                    case 3:
                        CreateLeaderboardEntry(thirdLeaderboardEntryPrefab, index, entry.coins, entry.displayName);
                        break;
                    default:
                        CreateLeaderboardEntry(leaderboardEntryPrefab, index, entry.coins, entry.displayName);
                        break;
                }
            }
        }

        private void CreateLeaderboardEntry(GameObject prefab, int rank, long coins, string username)
        {
            var go = Instantiate(prefab, parent);
            go.GetComponent<LeaderboardEntry>().SetLeaderboardEntryData(rank, coins, username);
        }

        private void DeleteAllChildren()
        {
            int childCount = parent.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject child = parent.GetChild(i).gameObject;
                Destroy(child);
            }
        }
    }
}