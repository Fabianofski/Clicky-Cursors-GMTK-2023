// /**
//  * This file is part of: GMTK-2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityEngine;

namespace F4B1.UI
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private string prefix;
        [SerializeField] private string suffix;
        
        public void SetIntText(int value)
        {
            textField.text = value + "";
        }

        public void SetIntTextWithText(int value)
        {
            textField.text = prefix + value + "\n" + suffix;
        }
        
    }
}