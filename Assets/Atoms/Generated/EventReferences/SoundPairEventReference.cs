using System;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `SoundPair`. Inherits from `AtomEventReference&lt;SoundPair, SoundVariable, SoundPairEvent, SoundVariableInstancer, SoundPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SoundPairEventReference : AtomEventReference<
        SoundPair,
        SoundVariable,
        SoundPairEvent,
        SoundVariableInstancer,
        SoundPairEventInstancer>, IGetEvent 
    { }
}
