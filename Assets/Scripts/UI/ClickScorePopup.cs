// /**
//  * This file is part of: GMTK-2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using TMPro;
using UnityEngine;

namespace F4B1.UI
{
    public class ClickScorePopup : MonoBehaviour
    {
        [SerializeField] private LeanTweenType tweenType;
        [SerializeField] private float tweenTime;
        
        [SerializeField] private TextMeshProUGUI textField;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;

            LeanTween.scale(gameObject, Vector3.one, tweenTime  / 2).setEase(tweenType).setOnComplete(() =>
            {
                LeanTween.scale(gameObject, Vector3.zero, tweenTime  / 2).setEase(tweenType);
            });
            
            LeanTween.value(0, 1, tweenTime / 2).setEase(tweenType).setOnUpdate(UpdateAlpha).setOnComplete(() =>
            {
                LeanTween.value(1, 0, tweenTime  / 2).setOnUpdate(UpdateAlpha).setEase(tweenType);
            });
            
            Destroy(transform.parent.gameObject, tweenTime + 0.1f);
        }

        public void SetText(string text)
        {
            textField.text = text;
        }

        private void UpdateAlpha(float value)
        {
            Color color = textField.color;
            color.a = value;
            textField.color = color;
        }
    }
}