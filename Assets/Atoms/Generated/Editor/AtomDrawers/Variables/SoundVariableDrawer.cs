#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `F4B1.Audio.Sound`. Inherits from `AtomDrawer&lt;SoundVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SoundVariable))]
    public class SoundVariableDrawer : VariableDrawer<SoundVariable> { }
}
#endif
