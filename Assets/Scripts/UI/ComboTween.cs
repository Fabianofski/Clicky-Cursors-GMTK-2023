// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using F4B1.Audio;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.UI
{
    [Serializable]
    public class ComboSounds
    {
        public int comboScore;
        public Sound comboSound;
    }

    public class ComboTween : MonoBehaviour
    {
        [SerializeField] private Image cooldownProgressBar1;
        [SerializeField] private Image cooldownProgressBar2;
        [SerializeField] private Image levelProgressBar;
        [SerializeField] private Gradient levelProgressBarGradient;
        [SerializeField] private Gradient cooldownProgressBarGradient;

        [SerializeField] private TextMeshProUGUI comboTextField;
        [SerializeField] private GameObject comboTextParent;
        [SerializeField] private float pulseAmount;
        [SerializeField] private float maxPulseAmount;
        [SerializeField] private float pulseDuration;
        [SerializeField] private int maxComboColor;
        [SerializeField] private Gradient comboTextGradient;

        [SerializeField] private LeanTweenType comboTweenType;
        [SerializeField] private float tweenDuration = 0.3f;
        [SerializeField] private float scaleAmount;
        [SerializeField] private float scaleTime;

        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private ComboSounds[] comboSounds;
        [SerializeField] private Sound loosingComboSound;
        [SerializeField] private float extraPitch;
        private LTDescr activePulseTween;
        private LTDescr activeScaleTween;

        private LTDescr activeTween;
        private int lastComboValue;

        private IEnumerator Pulse()
        {
            if (activePulseTween != null && LeanTween.isTweening(activeScaleTween.id))
            {
                LeanTween.cancel(activePulseTween.uniqueId);
                yield return null;
            }

            comboTextParent.transform.localScale = Vector3.one;
            var amount = 1 + Mathf.Min(maxPulseAmount, pulseAmount * lastComboValue);
            activePulseTween = LeanTween.scale(comboTextParent, Vector3.one * amount, pulseDuration)
                .setEasePunch();
        }

        public void ComboLevelChanged(float value)
        {
            StartCoroutine(nameof(Pulse));
            if (value > levelProgressBar.fillAmount)
            {
                LeanTween.value(levelProgressBar.fillAmount, value, tweenDuration)
                    .setOnUpdate(
                        val => { levelProgressBar.fillAmount = val; })
                    .setEase(comboTweenType);
            }
            else
            {
                var distanceToOne = 1 - levelProgressBar.fillAmount;
                var durationToOne = distanceToOne / (distanceToOne + value);
                if (distanceToOne == 0 && value == 0) durationToOne = 0;
                LeanTween.value(levelProgressBar.fillAmount, 1, tweenDuration * durationToOne)
                    .setOnUpdate(
                        val => { levelProgressBar.fillAmount = val % 1; })
                    .setOnComplete(() =>
                    {
                        levelProgressBar.fillAmount = 0;
                        LeanTween.value(0, value, tweenDuration * (1 - durationToOne))
                            .setOnUpdate(
                                val => { levelProgressBar.fillAmount = val; }).setEase(comboTweenType);
                    })
                    .setEase(comboTweenType);
            }

            var alpha = levelProgressBar.color.a;
            var color = levelProgressBarGradient.Evaluate(value);
            color.a = alpha;
            levelProgressBar.color = color;
        }

        public void CooldownLevelChanged(float value)
        {
            cooldownProgressBar1.fillAmount = value;
            cooldownProgressBar2.fillAmount = value;

            var alpha = cooldownProgressBar1.color.a;
            var color = cooldownProgressBarGradient.Evaluate(1 - value);
            color.a = alpha;
            cooldownProgressBar1.color = color;
            cooldownProgressBar2.color = color;
        }

        public void ComboIncreased(int value)
        {
            if (lastComboValue < value)
            {
                if (activeScaleTween != null && LeanTween.isTweening(activeScaleTween.id))
                {
                    LeanTween.cancel(activeScaleTween.uniqueId);
                    activeScaleTween.setOnComplete(() =>
                    {
                        gameObject.transform.localScale = Vector3.one;
                        activeScaleTween = LeanTween.scale(gameObject, Vector3.one * scaleAmount, scaleTime)
                            .setEasePunch();
                    });
                }
                else
                {
                    gameObject.transform.localScale = Vector3.one;
                    activeScaleTween = LeanTween.scale(gameObject, Vector3.one * scaleAmount, scaleTime).setEasePunch();
                }

                foreach (var comboSound in comboSounds)
                {
                    if (comboSound.comboScore > value) continue;

                    comboSound.comboSound.randomlyPitchSound = true;
                    var scoreDiff = value - comboSound.comboScore;
                    var pitch = 1 + Mathf.Min(5, scoreDiff) * extraPitch;
                    comboSound.comboSound.pitchBounds = new Vector2(pitch, pitch);
                    soundEvent.Raise(comboSound.comboSound);
                    break;
                }
            }
            else if (value > lastComboValue)
            {
                soundEvent.Raise(loosingComboSound);
            }

            comboTextField.text = "x" + value;

            var alpha = comboTextField.color.a;
            var color = comboTextGradient.Evaluate(Mathf.Min(1, value / (float)maxComboColor));
            color.a = alpha;
            comboTextField.color = color;

            lastComboValue = value;
        }
    }
}