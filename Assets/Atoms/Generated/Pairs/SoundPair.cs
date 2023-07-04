using System;
using UnityEngine;
using F4B1.Audio;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;F4B1.Audio.Sound&gt;`. Inherits from `IPair&lt;F4B1.Audio.Sound&gt;`.
    /// </summary>
    [Serializable]
    public struct SoundPair : IPair<F4B1.Audio.Sound>
    {
        public F4B1.Audio.Sound Item1 { get => _item1; set => _item1 = value; }
        public F4B1.Audio.Sound Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private F4B1.Audio.Sound _item1;
        [SerializeField]
        private F4B1.Audio.Sound _item2;

        public void Deconstruct(out F4B1.Audio.Sound item1, out F4B1.Audio.Sound item2) { item1 = Item1; item2 = Item2; }
    }
}