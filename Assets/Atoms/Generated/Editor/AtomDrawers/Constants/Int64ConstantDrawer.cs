#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `System.Int64`. Inherits from `AtomDrawer&lt;Int64Constant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(Int64Constant))]
    public class Int64ConstantDrawer : VariableDrawer<Int64Constant> { }
}
#endif
