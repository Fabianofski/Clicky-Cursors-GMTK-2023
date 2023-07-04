using UnityEditor;
using UnityAtoms.Editor;
using F4B1.Audio;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `F4B1.Audio.Sound`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(SoundVariable))]
    public sealed class SoundVariableEditor : AtomVariableEditor<F4B1.Audio.Sound, SoundPair> { }
}
