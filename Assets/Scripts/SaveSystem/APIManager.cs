// /**
//  * This file is part of: GMTK-2023
//  * Created: 13.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace F4B1.SaveSystem
{
    public static class APIManager
    {
        private const string URL = "http://localhost:3001";

        public static IEnumerator FetchGet(string path, Action<string> callback)
        {
            var endpoint = $"{URL}/{path}";

            using var webRequest = UnityWebRequest.Get(endpoint);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var response = webRequest.downloadHandler.text;
                callback?.Invoke(response);
            }
            else
            {
                Debug.LogError($"Error fetching {path}: " + webRequest.error);
                callback?.Invoke(null);
            }
        }
    }
}