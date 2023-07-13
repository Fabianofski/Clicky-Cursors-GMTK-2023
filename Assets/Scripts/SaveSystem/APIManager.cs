// /**
//  * This file is part of: GMTK-2023
//  * Created: 13.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace F4B1.SaveSystem
{
    public static class APIManager
    {
        private const string URL = "http://localhost:3000";
        private const string APIKey = "";
        
        public static string username { get; set; }
        public static string password { get; set; }

        public static bool isLoggedIn()
        {
            password = "12345";
            username = "hans";
            return username != "" && password != "";
        }

        public static IEnumerator FetchSaveData(Action<SaveData> callback)
        {
            var endpoint = $"{URL}/api/load?username={username}&password={password}&apiKey={APIKey}";

            using var webRequest = UnityWebRequest.Get(endpoint);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                Debug.Log(response);
                callback?.Invoke(JsonConvert.DeserializeObject<SaveData>(response));
            }
            else
            {
                Debug.LogError($"Error fetching Save Data: " + webRequest.error);
                callback?.Invoke(null);
            }
        }
        
        public static IEnumerator PostSaveData(SaveData data, Action<string> callback)
        {
            var endpoint = $"{URL}/api/save?username={username}&password={password}&apiKey={APIKey}";

            using var webRequest = new UnityWebRequest(endpoint, "POST");
            var body = JsonConvert.SerializeObject(data);
            byte[] byteData = new System.Text.UTF8Encoding().GetBytes(body);
            webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(byteData);
            webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback?.Invoke(response);
            }
            else
            {
                Debug.LogError($"Error saving Data: " + webRequest.error);
                callback?.Invoke(null);
            }
        }
    }
}