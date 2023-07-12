#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `System.Int64`. Inherits from `AtomDrawer&lt;Int64Variable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(Int64Variable))]
    public class Int64VariableDrawer : VariableDrawer<Int64Variable> { }
}
#endif
