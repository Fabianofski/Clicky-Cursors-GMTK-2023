// /**
//  * This file is part of: GMTK-2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using F4B1.Audio;
using F4B1.Core.Cookie;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cursor
{
    public class CursorClicker : MonoBehaviour
    {
        [SerializeField] private Transform cursorPos;
        [SerializeField] private Vector2 hitBox;
        [SerializeField] private LayerMask mask;
        [SerializeField] private Transform loadingBar;

        [SerializeField] private int scoreAmount;
        [SerializeField] private float critChance;
        [SerializeField] private float cooldown;
        [SerializeField] private IntEvent increaseComboEvent;

        [SerializeField] private SoundEvent clickSoundEvent;
        [SerializeField] private Sound[] clickSounds;
        private float cooldownTimer;

        private void Awake()
        {
            cooldownTimer = cooldown + Random.Range(0f, 1f) * cooldown;
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
            PerformClick();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(cursorPos.position, hitBox);
        }

        private void PerformClick()
        {
            LeanTween.scale(gameObject, new Vector3(0.6f, 0.7f, 0.7f), Mathf.Min(0.3f, cooldown / 2)).setEasePunch();

            clickSoundEvent.Raise(clickSounds[Random.Range(0, clickSounds.Length - 1)]);

            var col = Physics2D.OverlapBox(cursorPos.position, hitBox, 0, mask);
            if (!col) return;
            col.GetComponent<CookieScoreManager>().Click(scoreAmount, cursorPos.position);
            increaseComboEvent.Raise(1);
        }
    }
}