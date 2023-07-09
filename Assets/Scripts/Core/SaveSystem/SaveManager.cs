// /**
//  * This file is part of: GMTK-2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace F4B1.Core.SaveSystem
{
    public class SaveManager
    {
        public void SaveGame(SaveData data)
        {
            string json = JsonConvert.SerializeObject(data);
            string savePath = Path.Combine(Application.persistentDataPath, "clickyCursor.json");
            File.WriteAllText(savePath, json);
        }

        public SaveData LoadGame()
        {
            string savePath = Path.Combine(Application.persistentDataPath, "clickyCursor.json");
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                return JsonConvert.DeserializeObject<SaveData>(json);
            }
            
            return null;
        }
    }
}