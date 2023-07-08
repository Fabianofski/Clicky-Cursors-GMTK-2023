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
        
        [Header("Shop Button")]
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private TextMeshProUGUI costTextField;
        [SerializeField] private Image imageIcon;
        
        [Header("Shop Tooltip")]
        [SerializeField] private TextMeshProUGUI descriptionTextField;
        [SerializeField] private TextMeshProUGUI effectTextField;
        
        [SerializeField] private float multiplier;
        
        private int itemCost;
        private ShopItem shopItem;

        private void OnEnable()
        {
            CookieScoreChanged(coins.Value);
        }

        public void SetButtonInformation(ShopItem item)
        {
            titleTextField.text = $"{item.title} (x0)";
            costTextField.text = item.cost + "";
            imageIcon.sprite = item.icon;
            
            descriptionTextField.text = item.description;
            effectTextField.text = item.effect;

            shopItem = item;
            itemCost = item.cost;
            
            if (item.purchases == null)
                item.purchases = ScriptableObject.CreateInstance<IntVariable>();
            
            CookieScoreChanged(coins.Value);
            
            button.onClick.AddListener(() => BuyItem(item.clickEvent));
        }

        public void CookieScoreChanged(int value)
        {
            var maxPurchasesNotReached = true;
            if(shopItem != null)
                maxPurchasesNotReached = shopItem.purchases.Value < shopItem.maxPurchases || shopItem.maxPurchases == -1;
            button.interactable = value >= itemCost && maxPurchasesNotReached;
        }
        
        private void BuyItem(VoidEvent clickEvent)
        {
            if (coins.Value < itemCost) return;
            shopItem.purchases.Value++;
            coins.Subtract(itemCost);

            titleTextField.text = $"{shopItem.title} (x{shopItem.purchases.Value})";
            itemCost = Mathf.RoundToInt(shopItem.cost * Mathf.Pow(multiplier, shopItem.purchases.Value));
            costTextField.text = itemCost + "";
            
            clickEvent.Raise();
        }
    }
}