// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.UI
{
    public class ShopButton: MonoBehaviour
    {

        [SerializeField] private IntVariable coins;
        
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private TextMeshProUGUI costTextField;
        [SerializeField] private Image imageIcon;

        [SerializeField] private float multiplier;
        
        private int itemCost;
        private int originalCost;
        private int purchases;

        private void OnEnable()
        {
            CookieScoreChanged(coins.Value);
        }

        public void SetButtonInformation(string title, int cost, VoidEvent clickEvent, Sprite icon)
        {
            titleTextField.text = title;
            costTextField.text = cost + "";
            imageIcon.sprite = icon;

            itemCost = cost;
            originalCost = cost;
            
            CookieScoreChanged(coins.Value);
            
            button.onClick.AddListener(() => BuyItem(clickEvent));
        }

        public void CookieScoreChanged(int value)
        {
            button.interactable = value >= itemCost;
        }
        
        private void BuyItem(VoidEvent clickEvent)
        {
            if (coins.Value < itemCost) return;
            coins.Subtract(itemCost);

            purchases++;
            itemCost = Mathf.RoundToInt(originalCost * Mathf.Pow(multiplier, purchases));
            costTextField.text = itemCost + "";
            
            clickEvent.Raise();
        }
    }
}