// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using F4B1.Audio;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace F4B1.UI.Shop
{
    public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Int64Variable coins;

        [Header("Shop Button")] 
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private TextMeshProUGUI costTextField;
        [SerializeField] private Button button;
        [SerializeField] private Image imageIcon;

        [Header("Shop Tooltip")] 
        [SerializeField] private GameObject tooltip;
        [SerializeField] private TextMeshProUGUI descriptionTextField;
        [SerializeField] private TextMeshProUGUI effectTextField;

        [Header("Sounds")] 
        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private Sound buySound;
        [SerializeField] private Sound clickSound;
        [SerializeField] private Sound hoverSound;

        [Header("Cost Multiplier")] 
        [SerializeField] private float multiplier;
        private long itemCost;
        private ShopItem shopItem;

        private void OnEnable()
        {
            CookieScoreChanged(coins.Value);
            if(shopItem != null)
                UpdateTextFields();
        }

        public void SetButtonInformation(ShopItem item)
        {
            if (item.purchases == null)
                item.purchases = ScriptableObject.CreateInstance<IntVariable>();
            shopItem = item;
            itemCost = item.cost;

            descriptionTextField.text = item.description;
            effectTextField.text = item.effect;

            UpdateTextFields();
            CookieScoreChanged(coins.Value);

            button.onClick.AddListener(() => BuyItem(item.clickEvent));
        }

        public void CookieScoreChanged(long value)
        {
            var maxPurchasesReached = shopItem != null && shopItem.purchases.Value >= shopItem.maxPurchases &&
                                      shopItem.maxPurchases != -1;
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

            UpdateTextFields();

            if (clickEvent) clickEvent.Raise();
        }

        public void UpdateTextFields()
        {
            imageIcon.sprite = shopItem.icon;
            titleTextField.text = $"{shopItem.title} (x{shopItem.purchases.Value})";
            itemCost = (long) (shopItem.cost * Mathf.Pow(multiplier, Mathf.Min(30, shopItem.purchases.Value)));
            costTextField.text = NumberFormatter.FormatNumberWithLetters(itemCost);

            button.interactable = coins.Value >= itemCost;
            var maxPurchasesReached = shopItem.purchases.Value >= shopItem.maxPurchases && shopItem.maxPurchases != -1;
            if (!maxPurchasesReached) return;
            costTextField.text = "MAX";
            button.interactable = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetActive(true);
            soundEvent.Raise(hoverSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.SetActive(false);
        }
    }
}