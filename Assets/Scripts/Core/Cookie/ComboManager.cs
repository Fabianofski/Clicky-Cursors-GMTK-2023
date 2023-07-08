// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class ComboManager : MonoBehaviour
    {

        [SerializeField] private IntVariable combo;
        [SerializeField] private float comboCooldown;
        [SerializeField] private float comboCooldownMultiplier;
        private float comboCooldownTimer;
        private float comboCooldownTimerPassed;
        
        [SerializeField] private float levelUpAmount;
        [SerializeField] private float levelUpMultiplier;
        [SerializeField] private float comboLevelUp;
        [SerializeField] private float comboLevelAmount;

        [SerializeField] private FloatEvent cooldownProgress;
        [SerializeField] private FloatEvent levelUpProgress;

        private bool resetted;
        
        private void Awake()
        {
            comboLevelUp = levelUpAmount;
        }

        private void Update()
        {
            comboCooldownTimerPassed -= Time.deltaTime;

            cooldownProgress.Raise(comboCooldownTimerPassed / comboCooldownTimer);

            if (comboCooldownTimerPassed > 0 || resetted) return;
            
            resetted = true;
            combo.SetValue(0);
            comboLevelAmount = 0;
            levelUpProgress.Raise(0);
        }

        public void IncreaseCombo(int amount = 1)
        {
            resetted = false;
            
            comboCooldownTimer = comboCooldown * (1 / Mathf.Pow(comboCooldownMultiplier, combo.Value));
            comboCooldownTimerPassed = comboCooldownTimer;
            
            comboLevelUp = Mathf.CeilToInt(levelUpAmount * Mathf.Pow(levelUpMultiplier, combo.Value));
            comboLevelAmount += amount;
            levelUpProgress.Raise(comboLevelAmount / comboLevelUp);

            while (comboLevelAmount >= comboLevelUp)
            {
                comboLevelAmount -= comboLevelUp;
                combo.Add(1);
            }
        }
    }
}