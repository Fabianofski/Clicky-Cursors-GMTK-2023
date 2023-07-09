// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using F4B1.Audio;
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

        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private Sound buySound;
        [SerializeField] private Sound clickSound;
        
        
        private int itemCost;
        private ShopItem shopItem;

        private void OnEnable()
        {
            CookieScoreChanged(coins.Value);
        }

        public void SetButtonInformation(ShopItem item)
        {
            titleTextField.text = $"{item.title} (x0)";
            costTextField.text = NumberFormatter.FormatNumberWithLetters(item.cost);
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
            var maxPurchasesReached = shopItem != null && shopItem.purchases.Value >= shopItem.maxPurchases && shopItem.maxPurchases != -1;
            if (maxPurchasesReached) return;
            button.interactable = value >= itemCost;
        }
        
        private void BuyItem(VoidEvent clickEvent)
        {
            if (coins.Value < itemCost) return;
            shopItem.purchases.Value++;
            coins.Subtract(itemCost);
            soundEvent.Raise(buySound);
            soundEvent.Raise(clickSound);

            titleTextField.text = $"{shopItem.title} (x{shopItem.purchases.Value})";
            itemCost = Mathf.RoundToInt(shopItem.cost * Mathf.Pow(multiplier, shopItem.purchases.Value));
            costTextField.text = NumberFormatter.FormatNumberWithLetters(itemCost);
            
            var maxPurchasesReached = shopItem.purchases.Value >= shopItem.maxPurchases && shopItem.maxPurchases != -1;
            if (maxPurchasesReached)
            {
                costTextField.text = "MAX";
                button.interactable = false;
            }
            
            clickEvent.Raise();
        }
    }
}