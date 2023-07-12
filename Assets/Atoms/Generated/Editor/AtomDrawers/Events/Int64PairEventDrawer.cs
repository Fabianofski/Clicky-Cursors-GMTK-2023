#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Int64Pair`. Inherits from `AtomDrawer&lt;Int64PairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(Int64PairEvent))]
    public class Int64PairEventDrawer : AtomDrawer<Int64PairEvent> { }
}
#endif
