// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 14.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityEngine;

namespace F4B1.SaveSystem
{
    public class LoggedInInfo : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI usernameTextField;
        [SerializeField] private TextMeshProUGUI loggedInTextField;


        public void SetLoggedInInfo(string username)
        {
            usernameTextField.text = username;
            loggedInTextField.text = username != "" ? "Logged in as:" : "Not logged in!";
        }
        
    }
}