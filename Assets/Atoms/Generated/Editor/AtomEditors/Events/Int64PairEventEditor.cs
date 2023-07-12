#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using System;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Int64Pair`. Inherits from `AtomEventEditor&lt;Int64Pair, Int64PairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(Int64PairEvent))]
    public sealed class Int64PairEventEditor : AtomEventEditor<Int64Pair, Int64PairEvent> { }
}
#endif
