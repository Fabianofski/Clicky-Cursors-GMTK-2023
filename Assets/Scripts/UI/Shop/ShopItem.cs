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
        public string id;
        public string title;
        public string description;
        public string effect;
        public Sprite icon;
        
        public int maxPurchases = -1;
        public int cost;
        
        public VoidEvent clickEvent;
        public IntVariable purchases;
    }
}