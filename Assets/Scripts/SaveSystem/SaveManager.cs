// /**
//  * This file is part of: GMTK-2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace F4B1.SaveSystem
{
    public static class SaveManager
    {
        private const string SaveKey = "clickyCursorData";

        public static IEnumerator SaveGame(SaveData data, Action<string> callback)
        {
            if (APIManager.isLoggedIn())
                return APIManager.PostSaveData(data, callback);
            
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
            callback("Saved data locally!");

            return null;
        }

        public static IEnumerator LoadGame(Action<SaveData> callback)
        {
            if (APIManager.isLoggedIn())
                return APIManager.FetchSaveData(callback);

            if (!PlayerPrefs.HasKey(SaveKey)) return null;
            
            var json = PlayerPrefs.GetString(SaveKey);
            callback(JsonConvert.DeserializeObject<SaveData>(json));

            return null;
        }
    }
}