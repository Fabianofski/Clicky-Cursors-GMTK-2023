// /**
//  * This file is part of: GMTK-2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;

namespace F4B1.SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public long coins;
        public Dictionary<string, int> buildingItems;
        public Dictionary<string, int> cursorItems;
        public Dictionary<string, int> recipeItems;
        public DateTime timestamp;
        public string displayName;

        public SaveData()
        {
            buildingItems = new Dictionary<string, int>();
            cursorItems = new Dictionary<string, int>();
            recipeItems = new Dictionary<string, int>();
        }
    }
}