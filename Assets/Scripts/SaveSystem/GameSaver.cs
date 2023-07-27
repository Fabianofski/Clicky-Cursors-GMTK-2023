// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using System.Linq;
using F4B1.UI.Shop;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace F4B1.SaveSystem
{
    public class GameSaver : MonoBehaviour
    {
        [Header("Saved Items")]
        [SerializeField] private ShopItemValueList buildingItems;
        [SerializeField] private ShopItemValueList recipeItems;
        [SerializeField] private ShopItemValueList hardwareItems;
        [SerializeField] private Int64Variable coins;
        [SerializeField] private IntVariable combo;
        [SerializeField] private VoidEvent gameLoadedEvent;

        [Header("Global Save")] 
        [SerializeField] private int globalSaveCooldown;
        private float globalSaveTimer;

        [Header("Atoms")]
        [SerializeField] private StringVariable usernameVariable;
        [SerializeField] private BoolEvent offlineChangedEvent;

        private bool loaded;

        private void Start()
        {
            if (PlayerPrefs.HasKey("username") && APIManager.isLoggedIn())
                usernameVariable.SetValue(PlayerPrefs.GetString("username"));
            LoadGame();
        }

        private void Update()
        {
            globalSaveTimer -= Time.deltaTime;
        }

        public void ClearData()
        {
            PlayerPrefs.DeleteAll();
            
            ClearItems(buildingItems.List);
            ClearItems(recipeItems.List);
            ClearItems(hardwareItems.List);
            coins.SetValue(10);
            combo.SetValue(0);
            LeanTween.cancelAll();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void ClearItems(List<ShopItem> items)
        {
            foreach (var saveItem in items) saveItem.purchases.SetValue(0);
        }

        private void LoadGame()
        {
            var coroutine = SaveManager.LoadGame(GameLoadedCallback, exception =>
            {
                Debug.Log(exception);
                offlineChangedEvent.Raise(true);
            });
            if(coroutine != null) StartCoroutine(coroutine);
            else GameLoadedCallback(null);
        }

        private void GameLoadedCallback(SaveData onlineData)
        {
            if(onlineData != null) offlineChangedEvent.Raise(false);
            var localData = SaveManager.LoadLocalGame();

            SaveData data;
            if (localData == null && onlineData == null)
                data = new SaveData()
                {
                    coins = coins.InitialValue,
                };
            else if (localData == null)
                data = onlineData;
            else if (onlineData == null)
                data = localData;
            else
               data = localData.timestamp < onlineData.timestamp ? onlineData : localData;
            
            coins.SetValue(data.coins);

            BuyItems(buildingItems.List, data.buildingItems);
            BuyItems(recipeItems.List, data.recipeItems);
            BuyItems(hardwareItems.List, data.cursorItems);
            
            gameLoadedEvent.Raise();
            loaded = true;
        }

        private void BuyItems(List<ShopItem> items, Dictionary<string, int> dict)
        {
            foreach (var saveItem in items)
            {
                var count = dict.TryGetValue(saveItem.id, out var value) ? value : 0;
                saveItem.purchases.SetValue(count);
            }
        }

        public void SaveGame()
        {
            if (!loaded) return;

            var data = new SaveData
            {
                buildingItems = CreateDictionary(buildingItems.List),
                recipeItems = CreateDictionary(recipeItems.List),
                cursorItems = CreateDictionary(hardwareItems.List),
                coins = coins.Value,
                timestamp = DateTime.UtcNow,
                displayName = APIManager.username
            };
            
            SaveManager.SaveLocalGame(data);

            if (globalSaveTimer > 0) return;
            globalSaveTimer = globalSaveCooldown;

            var coroutine = SaveManager.SaveGame(data, message =>
            {
                offlineChangedEvent.Raise(false);
            }, exception =>
            {
                Debug.Log(exception);
                offlineChangedEvent.Raise(true);
            });
            if (coroutine != null) StartCoroutine(coroutine);
        }

        private Dictionary<string, int> CreateDictionary(List<ShopItem> items)
        {
            return items.ToDictionary(saveItem => saveItem.id, saveItem => saveItem.purchases.Value);
        }
    }
}