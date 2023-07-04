using System;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `F4B1.Audio.Sound`. Inherits from `AtomEventReference&lt;F4B1.Audio.Sound, SoundVariable, SoundEvent, SoundVariableInstancer, SoundEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SoundEventReference : AtomEventReference<
        F4B1.Audio.Sound,
        SoundVariable,
        SoundEvent,
        SoundVariableInstancer,
        SoundEventInstancer>, IGetEvent 
    { }
}
