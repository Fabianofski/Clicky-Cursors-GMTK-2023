using UnityEngine;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `SoundPair`. Inherits from `AtomEvent&lt;SoundPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/SoundPair", fileName = "SoundPairEvent")]
    public sealed class SoundPairEvent : AtomEvent<SoundPair>
    {
    }
}
