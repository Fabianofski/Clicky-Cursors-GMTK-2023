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
using UnityEngine.SceneManagement;

namespace F4B1.SaveSystem
{
    [Serializable]
    public class SaveItem
    {
        public string id;
        public IntVariable count;
    }

    public class GameSaver : MonoBehaviour
    {
        [Header("Saved Items")]
        [SerializeField] private SaveItem[] buildingItems;
        [SerializeField] private SaveItem[] recipeItems;
        [SerializeField] private SaveItem[] cursorItems;
        [SerializeField] private Int64Variable coins;
        [SerializeField] private IntVariable combo;
        [SerializeField] private VoidEvent gameLoadedEvent;

        
        [SerializeField] private StringVariable usernameVariable;

        private bool loaded;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("username"))
                usernameVariable.SetValue(PlayerPrefs.GetString("username"));
            LoadGame();
        }

        public void ClearData()
        {
            PlayerPrefs.DeleteAll();
            
            ClearItems(buildingItems);
            ClearItems(recipeItems);
            ClearItems(cursorItems);
            coins.SetValue(10);
            combo.SetValue(0);
            LeanTween.cancelAll();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void ClearItems(SaveItem[] items)
        {
            foreach (var saveItem in items) saveItem.count.SetValue(0);
        }

        private void LoadGame()
        {
            StartCoroutine(SaveManager.LoadGame(GameLoadedCallback));
        }

        private void GameLoadedCallback(SaveData onlineData)
        {
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
            Debug.Log(localData + " " + onlineData);
            
            coins.SetValue(data.coins);

            BuyItems(buildingItems, data.buildingItems);
            BuyItems(recipeItems, data.recipeItems);
            BuyItems(cursorItems, data.cursorItems);
            
            gameLoadedEvent.Raise();
            loaded = true;
        }

        private void BuyItems(SaveItem[] items, Dictionary<string, int> dict)
        {
            foreach (var saveItem in items)
            {
                var count = dict.ContainsKey(saveItem.id) ? dict[saveItem.id] : 0;
                saveItem.count.SetValue(count);
            }
        }

        public void SaveGame()
        {
            if (!loaded) return;

            var data = new SaveData
            {
                buildingItems = CreateDictionary(buildingItems),
                recipeItems = CreateDictionary(recipeItems),
                cursorItems = CreateDictionary(cursorItems),
                coins = coins.Value,
                timestamp = DateTime.UtcNow
            };
            
            SaveManager.SaveLocalGame(data);
            var enumerator = SaveManager.SaveGame(data, Debug.Log);
            if (enumerator == null) return;
            StartCoroutine(enumerator);
        }

        private Dictionary<string, int> CreateDictionary(SaveItem[] items)
        {
            return items.ToDictionary(saveItem => saveItem.id, saveItem => saveItem.count.Value);
        }
    }
}