using System;
using UnityAtoms.BaseAtoms;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Reference of type `F4B1.Audio.Sound`. Inherits from `AtomReference&lt;F4B1.Audio.Sound, SoundPair, SoundConstant, SoundVariable, SoundEvent, SoundPairEvent, SoundSoundFunction, SoundVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SoundReference : AtomReference<
        F4B1.Audio.Sound,
        SoundPair,
        SoundConstant,
        SoundVariable,
        SoundEvent,
        SoundPairEvent,
        SoundSoundFunction,
        SoundVariableInstancer>, IEquatable<SoundReference>
    {
        public SoundReference() : base() { }
        public SoundReference(F4B1.Audio.Sound value) : base(value) { }
        public bool Equals(SoundReference other) { return base.Equals(other); }
        protected override bool ValueEquals(F4B1.Audio.Sound other)
        {
            throw new NotImplementedException();
        }
    }
}
