// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieUpgradeManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] cookies;
        [SerializeField] private IntVariable currentCookieSize;

        public void IncreaseCookieSize()
        {
            SetCookiesActive();
        }

        public void SetCookiesActive()
        {
            for (var index = 0; index < cookies.Length; index++)
            {
                var cookie = cookies[index];
                cookie.SetActive(index == currentCookieSize.Value);
            }
        }
    }
}