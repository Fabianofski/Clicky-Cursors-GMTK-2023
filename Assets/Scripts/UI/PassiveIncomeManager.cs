// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.UI
{
    [Serializable]
    public class PassiveIncomeItem
    {
        public IntVariable countVariable;
        public int income;
    }

    public class PassiveIncomeManager : MonoBehaviour
    {
        [SerializeField] private IntVariable totalPassiveIncome;
        [SerializeField] private IntVariable coins;
        [SerializeField] private PassiveIncomeItem[] passiveIncomeItems;

        private float countdown = 1;

        private void Start()
        {
            CalculateTotalPassiveIncome();
        }

        private void Update()
        {
            countdown -= Time.deltaTime;

            if (countdown > 0) return;

            coins.Add(totalPassiveIncome.Value);
            countdown = 1;
        }

        public void CalculateTotalPassiveIncome()
        {
            var passiveIncome = passiveIncomeItems.Sum(passiveIncomeItem =>
                passiveIncomeItem.income * passiveIncomeItem.countVariable.Value);

            totalPassiveIncome.SetValue(passiveIncome);
        }
    }
}