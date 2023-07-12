#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using System;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `System.Int64`. Inherits from `AtomEventEditor&lt;System.Int64, Int64Event&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(Int64Event))]
    public sealed class Int64EventEditor : AtomEventEditor<System.Int64, Int64Event> { }
}
#endif
