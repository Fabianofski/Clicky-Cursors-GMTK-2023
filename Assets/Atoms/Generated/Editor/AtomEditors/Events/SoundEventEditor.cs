#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `F4B1.Audio.Sound`. Inherits from `AtomEventEditor&lt;F4B1.Audio.Sound, SoundEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(SoundEvent))]
    public sealed class SoundEventEditor : AtomEventEditor<F4B1.Audio.Sound, SoundEvent> { }
}
#endif
