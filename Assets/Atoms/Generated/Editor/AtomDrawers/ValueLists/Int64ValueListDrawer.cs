#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `System.Int64`. Inherits from `AtomDrawer&lt;Int64ValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(Int64ValueList))]
    public class Int64ValueListDrawer : AtomDrawer<Int64ValueList> { }
}
#endif
