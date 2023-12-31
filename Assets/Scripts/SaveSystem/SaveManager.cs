﻿// /**
//  * This file is part of: ClickyCursors-GMTK2023
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
        private static string saveKey => APIManager.username + "clickyCursorData";

        public static IEnumerator SaveGame(SaveData data, Action<string> callback, Action<Exception> exceptionCallback)
        {
            return APIManager.isLoggedIn() ? APIManager.PostSaveData(data, callback, exceptionCallback) : null;
        }

        public static void SaveLocalGame(SaveData data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
        }

        public static IEnumerator LoadGame(Action<SaveData> callback, Action<Exception> exceptionCallback)
        {
            return APIManager.isLoggedIn() ? APIManager.FetchSaveData(callback, exceptionCallback) : null;
        }

        public static SaveData LoadLocalGame()
        {
            if (!PlayerPrefs.HasKey(saveKey)) return null;
            
            var json = PlayerPrefs.GetString(saveKey);
            return JsonConvert.DeserializeObject<SaveData>(json);
        }
    }
}