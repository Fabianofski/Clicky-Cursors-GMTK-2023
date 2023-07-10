// /**
//  * This file is part of: GMTK-2023
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private VoidEvent destroySoundEvent;

        public void PlaySound(Sound sound)
        {
            var audioObject = new GameObject { name = sound.clip.name };

            var source = audioObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.outputAudioMixerGroup = sound.outputAudioMixerGroup;
            source.volume = sound.volume;
            source.clip = sound.clip;

            if (sound.dontDestroyOnLoad) DontDestroyOnLoad(audioObject);
            if (sound.randomlyPitchSound)
                source.pitch = Random.Range(sound.pitchBounds.x, sound.pitchBounds.y);
            if (sound.destroySoundOnEvent) destroySoundEvent.Register(() => Destroy(audioObject));

            source.Play();
            Destroy(audioObject, sound.clip.length * 2f);
        }
    }
}