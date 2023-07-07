// /**
//  * This file is part of: GMTK-2023
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections;
using UnityEngine;

namespace F4B1.Audio
{
    public class Jukebox : MonoBehaviour
    {
        [SerializeField] private float fadeTime;
        private AudioSource _audioSource;
        private string _currentlyPlaying = "";

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (GameObject.FindGameObjectsWithTag("JukeBox").Length > 1)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        public void SwitchTrack(Sound sound)
        {
            if (_currentlyPlaying.Equals(sound.clip.name)) return;
            _currentlyPlaying = sound.clip.name;
            if (_audioSource.isPlaying)
            {
                StartCoroutine(FadeOutAndSwitchTrack(_audioSource, sound.clip, fadeTime));
            }
            else
            {
                _audioSource.clip = sound.clip;
                _audioSource.Play();
            }
        }

        private static IEnumerator FadeOutAndSwitchTrack(AudioSource audioSource, AudioClip newClip, float fadeTime)
        {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }

            audioSource.volume = startVolume;
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}