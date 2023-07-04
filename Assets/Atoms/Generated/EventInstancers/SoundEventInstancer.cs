using UnityEngine;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Instancer of type `F4B1.Audio.Sound`. Inherits from `AtomEventInstancer&lt;F4B1.Audio.Sound, SoundEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Sound Event Instancer")]
    public class SoundEventInstancer : AtomEventInstancer<F4B1.Audio.Sound, SoundEvent> { }
}
