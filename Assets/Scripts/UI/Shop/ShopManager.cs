// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.UI.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Shop Tabs")]
        [SerializeField] private Transform buildingsTab;
        [SerializeField] private Transform recipesTab;
        [SerializeField] private Transform hardwareTab;

        [Header("Items")]
        [SerializeField] private GameObject shopButtonPrefab;
        [SerializeField] private ShopItemValueList buildingItems;
        [SerializeField] private ShopItemValueList recipesItems;
        [SerializeField] private ShopItemValueList hardwareItems;

        private void Start()
        {
            InstantiateShopItems(hardwareItems.List, hardwareTab);
            InstantiateShopItems(recipesItems.List, recipesTab);
            InstantiateShopItems(buildingItems.List, buildingsTab);
            InstantiateShopItems(buildingItems.List, buildingsTab);
            InstantiateShopItems(buildingItems.List, buildingsTab);
        }

        private void InstantiateShopItems(List<ShopItem> items, Transform tab)
        {
            foreach (var item in items)
            {
                var go = Instantiate(shopButtonPrefab, tab);
                go.GetComponent<ShopButton>().SetButtonInformation(item);
            }
        }
    }
}