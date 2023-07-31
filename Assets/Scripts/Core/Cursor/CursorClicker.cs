// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using F4B1.Audio;
using F4B1.Core.Cookie;
using F4B1.Core.Pool;
using F4B1.UI;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cursor
{
    public class CursorClicker : MonoBehaviour
    {
        [Header("Cursor Components")]
        [SerializeField] private Transform cursorPos;
        [SerializeField] private Vector2 hitBox;
        [SerializeField] private LayerMask mask;
        [SerializeField] private Transform loadingBar;

        [Header("Click Score")]
        [SerializeField] private int scoreAmount;
        [SerializeField] private float critChance;
        [SerializeField] private float cooldown;
        [SerializeField] private IntEvent increaseComboEvent;
        
        [Header("Score Variables")]
        [SerializeField] private Int64Variable playerScoreVariable;
        [SerializeField] private IntVariable currentComboVariable;
        [SerializeField] private IntVariable cursorMultiplierVariable;
        private ClickScorePopupPool scorePopupPool;

        [Header("Sounds")]
        private TransformPool clickPool;
        private float cooldownTimer;

        private void Awake()
        {
            cooldownTimer = cooldown + Random.Range(0f, 1f) * cooldown;
            clickPool = GameObject.FindWithTag("ClickSoundObjectPool").GetComponent<TransformPool>();
            scorePopupPool = GameObject.FindWithTag("ScorePopupPool").GetComponent<ClickScorePopupPool>();
        }

        private void Update()
        {
            if (cooldownTimer / cooldown > 0.6)
                loadingBar.localScale = Vector3.zero;
            else
                loadingBar.localScale = Vector3.one * cooldownTimer;

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer > 0) return;

            cooldownTimer = cooldown;
            TryPerformClick();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(cursorPos.position, hitBox);
        }

        private void TryPerformClick()
        {
            LeanTween.scale(gameObject, new Vector3(0.6f, 0.7f, 0.7f), Mathf.Min(0.3f, cooldown / 2)).setEasePunch();

            var clickGo = clickPool.GetPooledGameObject();
            if(clickGo != null) clickGo.gameObject.SetActive(true);

            var col = Physics2D.OverlapBox(cursorPos.position, hitBox, 0, mask);
            if (!col) return;
            Click(scoreAmount, cursorPos.position);
            increaseComboEvent.Raise(1);
        }
        
        private void Click(int score, Vector2 pos)
        {
            var calculatedScore = score * Mathf.Max(1, currentComboVariable.Value) * (cursorMultiplierVariable.Value + 1);
            playerScoreVariable.Add(calculatedScore);

            var popup = scorePopupPool.GetPooledGameObject();
            if (popup == null) return;
            popup.transform.position = pos;
            popup.SetNumber(calculatedScore);
            popup.gameObject.SetActive(true);
        }
    }
}