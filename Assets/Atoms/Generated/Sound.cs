// /**
//  * This file is part of: GMTK-2023
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace F4B1.Audio
{
    [CreateAssetMenu(fileName = "new Sound", menuName = "Audio/newSound")]
    public class Sound : ScriptableObject
    {
        public AudioClip clip;
        public AudioMixerGroup outputAudioMixerGroup;
        [Range(0f, 1f)] public float volume = 1f;
        public bool dontDestroyOnLoad;

        public bool destroySoundOnEvent;
        [HideInInspector] public bool randomlyPitchSound;
        [HideInInspector] public Vector2 pitchBounds = Vector2.one;
    }

# if UNITY_EDITOR
    [CustomEditor(typeof(Sound))]
    public class MyScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var sound = target as Sound;


            sound.randomlyPitchSound = GUILayout.Toggle(sound.randomlyPitchSound, "Randomly Pitch Sound");

            if (sound.randomlyPitchSound)
                sound.pitchBounds = EditorGUILayout.Vector2Field("Pitch Boundaries:", sound.pitchBounds);
            else
                sound.pitchBounds = Vector2.one;
        }
    }
#endif
}