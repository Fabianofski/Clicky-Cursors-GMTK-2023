using System;
using UnityEngine.Events;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// None generic Unity Event of type `SoundPair`. Inherits from `UnityEvent&lt;SoundPair&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SoundPairUnityEvent : UnityEvent<SoundPair> { }
}
