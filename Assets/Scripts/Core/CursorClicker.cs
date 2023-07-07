// /**
//  * This file is part of: GMTK-2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using UnityEngine;

namespace F4B1.Core
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
        private float cooldownTimer;
        
        private void Awake()
        {
            cooldownTimer = cooldown;
        }

        private void Update()
        {
            loadingBar.localScale = Vector3.one * cooldownTimer;
            
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer > 0) return;
            
            cooldownTimer = cooldown;
            PerformClick();
        }

        private void PerformClick()
        {
            Collider2D col = Physics2D.OverlapBox(cursorPos.position, hitBox, 0, mask);
            if (!col) return;
            
            col.gameObject.GetComponent<CookieScoreManager>().Click(scoreAmount);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawWireCube(cursorPos.position, hitBox);
        }
    }
}