// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using F4B1.UI.Shop;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cursor
{
    public class CursorManager : MonoBehaviour
    {
        [Header("Cursors")] 
        [SerializeField] private ShopItemValueList cursorAtomList;
        private Dictionary<string, int> purchasedCursors;

        private void OnEnable()
        {
            purchasedCursors = new Dictionary<string, int>();
        }

        public void BuyRemainingCursors()
        {
            foreach (var shopItem in cursorAtomList.List)
            {
                var createdCursors = purchasedCursors.TryGetValue(shopItem.id, out var value) ? value : 0;
                var toBeCreated = shopItem.purchases.Value - createdCursors;
                for (var i = 0; i < toBeCreated; i++)
                    InstantiateCursor(shopItem);
            }
        }

        private void InstantiateCursor(ShopItem item)
        {
            Instantiate(item.prefab, transform);
            if (purchasedCursors.ContainsKey(item.id))
                purchasedCursors[item.id]++;
            else purchasedCursors[item.id] = 1;
        }
    }
}