// /**
//  * This file is part of: GMTK-2023
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using UnityEngine;

namespace F4B1.Audio
{
    [Serializable]
    public class MusicTrack
    {
        public AudioSource audioSource;
        public int comboScore;
    }
    
    public class Jukebox : MonoBehaviour
    {
        [SerializeField] private float fadeTime;
        [SerializeField] private MusicTrack[] layers;
        private float volume;
        
        private void Awake()
        {
            volume = layers[0].audioSource.volume;
            SwitchTrack(0);
        }

        public void SwitchTrack(int comboValue)
        {
            bool layerFound = false;
            foreach (var layer in layers)
            {
                if (layer.comboScore > comboValue || layerFound)
                {
                    layer.audioSource.volume = 0;
                    continue;
                }

                layerFound = true;
                layer.audioSource.volume = volume;
            }
        }

        private static IEnumerator FadeInTrack(AudioSource audioSource, float fadeTime)
        {
            var startVolume = audioSource.volume;
            audioSource.volume = 0;

            while (audioSource.volume < startVolume)
            {
                audioSource.volume += startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }

            audioSource.volume = startVolume;
            audioSource.Play();
        }
    }
}