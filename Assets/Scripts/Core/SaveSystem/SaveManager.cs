// /**
//  * This file is part of: GMTK-2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using Newtonsoft.Json;
using UnityEngine;

namespace F4B1.Core.SaveSystem
{
    public class SaveManager
    {
        private const string saveKey = "clickyCursorData";

        public static void SaveGame(SaveData data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
        }

        public static SaveData LoadGame()
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                var json = PlayerPrefs.GetString(saveKey);
                return JsonConvert.DeserializeObject<SaveData>(json);
            }

            return null;
        }
    }
}