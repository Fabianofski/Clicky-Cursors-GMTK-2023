using System;
using F4B1.Audio;
using F4B1.UI;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieScoreManager : MonoBehaviour
    {
        [Header("Score Variables")]
        [SerializeField] private Int64Variable playerScoreVariable;
        [SerializeField] private IntVariable currentComboVariable;
        [SerializeField] private IntVariable cursorMultiplierVariable;
        private ObjectPool scorePopupPool;


        private void Awake()
        {
            scorePopupPool = GameObject.FindWithTag("ScorePopupPool").GetComponent<ObjectPool>();
        }

        public void Click(int score, Vector2 pos)
        {
            var calculatedScore = score * Mathf.Max(1, currentComboVariable.Value) * (cursorMultiplierVariable.Value + 1);
            playerScoreVariable.Add(calculatedScore);

            var go = scorePopupPool.GetPooledGameObject();
            if (go == null) return;
            go.transform.position = pos;
            go.GetComponentInChildren<ClickScorePopup>().SetNumber(calculatedScore);
            go.SetActive(true);
        }
    }
}