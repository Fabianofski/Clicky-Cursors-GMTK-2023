// /**
//  * This file is part of: Golf, yes?
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Audio
{
    public class SongSwitcher : MonoBehaviour
    {
        [SerializeField] private Sound track;
        [SerializeField] private SoundEvent switchMusicTrack;

        private void Start()
        {
            switchMusicTrack.Raise(track);
        }
    }
}