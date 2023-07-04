#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `F4B1.Audio.Sound`. Inherits from `AtomDrawer&lt;SoundValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SoundValueList))]
    public class SoundValueListDrawer : AtomDrawer<SoundValueList> { }
}
#endif
