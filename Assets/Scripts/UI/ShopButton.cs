// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.UI
{
    public class ShopButton: MonoBehaviour
    {

        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private TextMeshProUGUI costTextField;
        
        public void SetButtonInformation(string title, int cost, VoidEvent clickEvent)
        {
            titleTextField.text = title;
            costTextField.text = cost + "";

            button.onClick.AddListener(clickEvent.Raise);
        }
    }
}