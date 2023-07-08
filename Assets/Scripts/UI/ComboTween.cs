// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.UI
{
    public class ComboTween : MonoBehaviour
    {

        [SerializeField] private Image cooldownProgressBar1;
        [SerializeField] private Image cooldownProgressBar2;
        [SerializeField] private Image levelProgressBar;
        [SerializeField] private TextMeshProUGUI comboTextField;
        
        [SerializeField] private LeanTweenType comboTweenType;
        [SerializeField] private float tweenDuration = 0.3f;

        private LTDescr activeTween;
        
        public void ComboLevelChanged(float value)
        {
            if(value > levelProgressBar.fillAmount)
                LeanTween.value(levelProgressBar.fillAmount, value, tweenDuration)
                    .setOnUpdate(
                        val =>
                        {
                            levelProgressBar.fillAmount = val;
                        })
                    .setEase(comboTweenType);
            else
            {
                var distanceToOne = 1 - levelProgressBar.fillAmount;
                var durationToOne = distanceToOne / (distanceToOne + value);
                if (distanceToOne == 0 && value == 0) durationToOne = 0;
                LeanTween.value(levelProgressBar.fillAmount, 1, tweenDuration * durationToOne)
                    .setOnUpdate(
                        val =>
                        {
                            levelProgressBar.fillAmount = val % 1;
                        })
                    .setOnComplete(() =>
                    {
                        levelProgressBar.fillAmount = 0;
                        LeanTween.value(0, value, tweenDuration * (1 - durationToOne))
                            .setOnUpdate(
                                val =>
                                {
                                    levelProgressBar.fillAmount = val;
                                }).setEase(comboTweenType);
                    })
                    .setEase(comboTweenType);
            }
        }

        public void CooldownLevelChanged(float value)
        {
            cooldownProgressBar1.fillAmount = value;
            cooldownProgressBar2.fillAmount = value;
        }

        public void ComboIncreased(int value)
        {
            comboTextField.text = "x" + value;
        }
        
    }
}