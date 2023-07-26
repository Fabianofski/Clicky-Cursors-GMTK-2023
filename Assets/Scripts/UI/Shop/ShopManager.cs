// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.UI.Shop
{
    [Serializable]
    public class ShopItem
    {
        public VoidEvent clickEvent;
        public IntVariable purchases;
        public int maxPurchases = -1;
        public string title;
        public int cost;
        public Sprite icon;
        public string description;
        public string effect;
    }

    public class ShopManager : MonoBehaviour
    {
        [Header("Shop Tabs")]
        [SerializeField] private Transform buildingsTab;
        [SerializeField] private Transform recipesTab;
        [SerializeField] private Transform hardwareTab;

        [Header("Items")]
        [SerializeField] private GameObject shopButtonPrefab;
        [SerializeField] private ShopItem[] buildingItems;
        [SerializeField] private ShopItem[] recipesItems;
        [SerializeField] private ShopItem[] hardwareItems;

        private void Start()
        {
            InstantiateShopItems(hardwareItems, hardwareTab);
            InstantiateShopItems(recipesItems, recipesTab);
            InstantiateShopItems(buildingItems, buildingsTab);
        }

        private void InstantiateShopItems(ShopItem[] items, Transform tab)
        {
            foreach (var item in items)
            {
                var go = Instantiate(shopButtonPrefab, tab);
                go.GetComponent<ShopButton>().SetButtonInformation(item);
            }
        }
    }
}