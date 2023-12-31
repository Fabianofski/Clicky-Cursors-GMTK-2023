﻿// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.UI.Shop
{
    public class PassiveIncomeManager : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private Int64Variable coins;
        [SerializeField] private IntVariable totalPassiveIncome;
        
        [Header("Items")]
        [SerializeField] private ShopItemValueList passiveIncomeItems;

        private float countdown = 1;

        private void Start()
        {
            CalculateTotalPassiveIncome();
        }

        private void Update()
        {
            countdown -= Time.deltaTime;

            if (countdown > 0) return;
            countdown = 1;
            if (totalPassiveIncome.Value == 0) return;

            coins.Add(totalPassiveIncome.Value);
        }

        public void CalculateTotalPassiveIncome()
        {
            var passiveIncome = passiveIncomeItems.List.Sum(passiveIncomeItem =>
                passiveIncomeItem.income * passiveIncomeItem.purchases.Value);

            totalPassiveIncome.SetValue(passiveIncome);
        }
    }
}