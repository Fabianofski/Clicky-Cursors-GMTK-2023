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
        private LTDescr activeScaleTween;

        public void SetIntText(int value)
        {
            var newText = NumberFormatter.FormatNumberWithLetters(value);

            if (newText == textField.text) return;
            
            textField.text = newText;
            if(activeScaleTween != null)
                LeanTween.cancel(activeScaleTween.id);
            gameObject.transform.localScale = Vector3.one;
            activeScaleTween = LeanTween.scale(gameObject, Vector3.one * 0.8f, 0.2f).setEasePunch();
        }

        public void SetIntTextWithText(int value)
        {
            textField.text = prefix + value + suffix;
        }
        
    }
}