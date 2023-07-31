// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 30.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Audio
{
    public class StandaloneSound : MonoBehaviour
    {
        [SerializeField] private VoidEvent destroySoundEvent;
        [SerializeField] private Sound[] sounds;
        private AudioSource source;

        public void OnEnable()
        {
            var sound = sounds[Random.Range(0, sounds.Length - 1)];
            
            if(source == null) source = gameObject.AddComponent<AudioSource>();
            
            source.playOnAwake = false;
            source.outputAudioMixerGroup = sound.outputAudioMixerGroup;
            source.volume = sound.volume;
            source.clip = sound.clip;

            if (sound.dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            if (sound.randomlyPitchSound)
                source.pitch = Random.Range(sound.pitchBounds.x, sound.pitchBounds.y);
            if (sound.destroySoundOnEvent) destroySoundEvent.Register(() => Destroy(gameObject));

            source.Play();
            Invoke(nameof(Disable), sound.clip.length * 2f);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}