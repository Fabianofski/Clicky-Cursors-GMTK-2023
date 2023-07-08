// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieUpgradeManager : MonoBehaviour
    {

        [SerializeField] private GameObject[] cookies;
        private int currentCookieSize;
        
        public void IncreaseCookieSize()
        {
            if (currentCookieSize >= cookies.Length - 1) return;
            cookies[currentCookieSize].SetActive(false);
            currentCookieSize++;
            cookies[currentCookieSize].SetActive(true);
        }
        
    }
}