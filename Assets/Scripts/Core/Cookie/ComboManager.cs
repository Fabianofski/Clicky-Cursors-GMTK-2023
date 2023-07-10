﻿// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class ComboManager : MonoBehaviour
    {
        [SerializeField] private IntVariable combo;
        [SerializeField] private float comboCooldown;
        [SerializeField] private float comboCooldownMultiplier;

        [SerializeField] private float levelUpAmount;
        [SerializeField] private float levelUpMultiplier;
        [SerializeField] private float comboLevelUp;
        [SerializeField] private float comboLevelAmount;

        [SerializeField] private FloatEvent cooldownProgress;
        [SerializeField] private FloatEvent levelUpProgress;
        private float comboCooldownTimer;
        private float comboCooldownTimerPassed;

        private void Awake()
        {
            comboLevelUp = levelUpAmount;
        }

        private void Update()
        {
            comboCooldownTimerPassed -= Time.deltaTime;

            if (combo.Value > 0)
                cooldownProgress.Raise(comboCooldownTimerPassed / comboCooldownTimer);
            else cooldownProgress.Raise(0);

            if (comboCooldownTimerPassed > 0) return;

            comboLevelAmount = 0;
            combo.Subtract(1);
            if (combo.Value < 0) combo.SetValue(0);
            IncreaseCombo(0);
        }

        public void IncreaseCombo(int amount = 1)
        {
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