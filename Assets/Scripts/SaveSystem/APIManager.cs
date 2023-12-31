﻿// /**
//  * This file is part of: ClickyCursors-GMTK2023
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
        private static readonly string URL;
        
        public static string username { get; set; }
        public static string password { get; set; }
        
        static APIManager()
        {
#if UNITY_EDITOR
            URL = "http://localhost:3000/clicky-cursors";
#else
            URL = "https://jamalyzer.com/clicky-cursors";
#endif
            username = PlayerPrefs.HasKey("username") ? PlayerPrefs.GetString("username") : "";
            password = "";
        }
        
        public static bool isLoggedIn()
        {
            return username != "" && password != "";
        }

        public static IEnumerator AttemptLogin(string name, string pwd, Action<bool> callback)
        {
            var endpoint = $"{URL}/api/login";

            using var webRequest = UnityWebRequest.Get(endpoint);
            var auth = $"{name}:{pwd}";
            var authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(auth));
            webRequest.SetRequestHeader("Authorization", authHeader);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback?.Invoke(JsonConvert.DeserializeObject<bool>(response));
            }
            else
            {
                Debug.LogError($"Error checking if user exists: " + webRequest.error);
                callback?.Invoke(false);
            }
        }
        
        public static IEnumerator FetchLeaderboard(Action<SaveData[]> callback)
        {
            var endpoint = $"{URL}/api/leaderboard/";

            using var webRequest = UnityWebRequest.Get(endpoint);
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "https://jamalyzer.com/");
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback?.Invoke(JsonConvert.DeserializeObject<SaveData[]>(response));
            }
            else
            {
                Debug.LogError($"Error fetching Leaderboard: " + webRequest.error);
                callback?.Invoke(null);
            }
        }
        
        public static IEnumerator UserExists(string name, Action<bool> callback, Action<Exception> exceptionCallback)
        {
            var endpoint = $"{URL}/api/userExists?username={name}";

            using var webRequest = UnityWebRequest.Get(endpoint);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback?.Invoke(JsonConvert.DeserializeObject<bool>(response));
            }
            else
            {
                Debug.LogError($"Error checking if user exists: " + webRequest.error);
                exceptionCallback?.Invoke(new Exception(webRequest.error));
            }
        }
        
        public static IEnumerator FetchSaveData(Action<SaveData> callback, Action<Exception> exceptionCallback)
        {
            var endpoint = $"{URL}/api/load";
            
            using var webRequest = UnityWebRequest.Get(endpoint);
            var auth = $"{username}:{password}";
            var authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(auth));
            webRequest.SetRequestHeader("Authorization", authHeader);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback?.Invoke(JsonConvert.DeserializeObject<SaveData>(response));
            }
            else
            {
                Debug.LogError($"Error fetching Save Data: " + webRequest.error);
                callback?.Invoke(null);
                exceptionCallback.Invoke(new Exception(webRequest.error));
            }
        }
        
        public static IEnumerator PostSaveData(SaveData data, Action<string> callback, Action<Exception> exceptionCallback)
        {
            var endpoint = $"{URL}/api/save";

            using var webRequest = new UnityWebRequest(endpoint, "POST");
            
            var body = JsonConvert.SerializeObject(data);
            byte[] byteData = new System.Text.UTF8Encoding().GetBytes(body);
            webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(byteData);
            webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            
            var auth = $"{username}:{password}";
            var authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(auth));
            webRequest.SetRequestHeader("Authorization", authHeader);
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback.Invoke(response);
            }
            else
            {
                exceptionCallback.Invoke(new Exception(webRequest.error));
            }
        }
    }
}