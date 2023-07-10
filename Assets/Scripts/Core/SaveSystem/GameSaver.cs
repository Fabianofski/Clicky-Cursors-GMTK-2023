// /**
//  * This file is part of: GMTK-2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.SaveSystem
{
    [Serializable]
    public class SaveItem
    {
        public string id;
        public IntVariable count;
    }

    public class GameSaver : MonoBehaviour
    {
        [SerializeField] private SaveItem[] buildingItems;
        [SerializeField] private SaveItem[] recipeItems;
        [SerializeField] private SaveItem[] cursorItems;
        [SerializeField] private IntVariable coins;

        private bool loaded;

        private void Awake()
        {
            LoadGame();
            loaded = true;
        }

        public void ClearData()
        {
            PlayerPrefs.DeleteKey("clickyCursorData");
        }

        private void LoadGame()
        {
            var data = SaveManager.LoadGame();
            if (data == null) return;

            coins.SetValue(data.coins);

            BuyItems(buildingItems, data.buildingItems);
            BuyItems(recipeItems, data.recipeItems);
            BuyItems(cursorItems, data.cursorItems);
        }

        private void ClearItems(SaveItem[] items)
        {
            foreach (var saveItem in items) saveItem.count.Reset();
        }

        private void BuyItems(SaveItem[] items, Dictionary<string, int> dict)
        {
            foreach (var saveItem in items) saveItem.count.SetValue(dict[saveItem.id]);
        }

        public void SaveGame()
        {
            if (!loaded) return;

            var data = new SaveData
            {
                buildingItems = CreateDictionary(buildingItems),
                recipeItems = CreateDictionary(recipeItems),
                cursorItems = CreateDictionary(cursorItems),
                coins = coins.Value
            };
            SaveManager.SaveGame(data);
        }

        private Dictionary<string, int> CreateDictionary(SaveItem[] items)
        {
            return items.ToDictionary(saveItem => saveItem.id, saveItem => saveItem.count.Value);
        }
    }
}