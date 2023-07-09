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
        public VoidEvent purchaseEvent;
    }
    
    public class GameSaver : MonoBehaviour
    {
        [SerializeField] private SaveItem[] buildingItems;
        [SerializeField] private SaveItem[] recipeItems;
        [SerializeField] private SaveItem[] cursorItems;
        [SerializeField] private IntVariable coins;
        private SaveManager saveManager;

        private void Awake()
        {
            saveManager = new SaveManager();
            
            // SaveGame();
            LoadGame();
        }


        private void LoadGame()
        {
            var data = saveManager.LoadGame();
            if (data == null) return;

            coins.SetValue(data.coins);
            
            BuyItems(buildingItems, data.buildingItems);
            BuyItems(recipeItems, data.recipeItems);
            BuyItems(cursorItems, data.cursorItems);
        }

        private void BuyItems(SaveItem[] items, Dictionary<string, int> dict)
        {
            foreach (var saveItem in items)
            {
                saveItem.count.SetValue(dict[saveItem.id]);
                for (int i = 0; i < dict[saveItem.id]; i++)
                {
                    saveItem.purchaseEvent.Raise();
                }
            }
        }
        
        private void SaveGame()
        {
            var data = new SaveData
            {
                buildingItems = CreateDictionary(buildingItems),
                recipeItems = CreateDictionary(recipeItems),
                cursorItems = CreateDictionary(cursorItems),
                coins = coins.Value
            };
            saveManager.SaveGame(data);
        }

        private Dictionary<string, int> CreateDictionary(SaveItem[] items)
        {
            return items.ToDictionary(saveItem => saveItem.id, saveItem => saveItem.count.Value);
        }
        
    }
}