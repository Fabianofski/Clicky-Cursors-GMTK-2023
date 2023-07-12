#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `System.Int64`. Inherits from `AtomDrawer&lt;Int64Event&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(Int64Event))]
    public class Int64EventDrawer : AtomDrawer<Int64Event> { }
}
#endif
