// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 26.07.2023
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
        [Header("General")]
        public string id;
        public string title;
        public string description;
        public string effect;
        public Sprite icon;
        
        [Header("Shop")]
        public int maxPurchases = -1;
        public int cost;
        
        [Header("Atoms")]
        public VoidEvent clickEvent;
        public IntVariable purchases;

        [Header("Optional")] 
        public GameObject prefab;
    }
}